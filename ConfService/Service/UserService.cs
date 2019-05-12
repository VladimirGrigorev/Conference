using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfService.Dto;
using ConfService.Helper;
using ConfService.Interface;
using ConfService.ServiceException;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ConfService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public UserService(IUserRepository userRepository, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public UserDto Get(int id)
        {
            return _mapper.Map<UserDto>(_userRepository.Get(id));
        }

        public UserInfoDto GetUserByEmail(string email)
        {
            if (_userRepository.GetFirstOrDefault(u => u.Email == email) is User user)
            {
                return _mapper.Map<UserInfoDto>(user);
            }

            throw new UserNotFoundException();
        }

        public int Add(UserDto userDto)
        {
            var user = _userRepository.GetFirstOrDefault(x => x.Email == userDto.Email);
            if (user != null)
                throw new UserWithThisEmailExistsException();

            user = _mapper.Map<User>(userDto);
            //user.IsGlobalAdmin = true;
            return _userRepository.Add(user);
        }

        public TokenDto Authenticate(UserAuthDto userDto)
        {
            var user = _userRepository.GetFirstOrDefaultWithRoles(
                x => x.Email == userDto.Email && x.PassHash == userDto.PassHash);

            if(user == null)
                throw new AuthenticationException();

            var expirationTime = DateTime.UtcNow.AddSeconds(_jwtSettings.LifetimeSeconds);
            var tokenDto = new TokenDto()
            {
                ExpirationTime = expirationTime,
                IsGlobalAdmin = user.IsGlobalAdmin,
                UserId = user.Id,
                AdminnedConferences = user.AdminOfConferences
                    .Select(a=>a.ConferenceId).ToList(),
                PresentedLectures = user.RoleInLectures
                    .Where(l=>l.Role == Role.Speaker)
                    .Select(l=>l.LectureId).ToList(),
                SubscribedLectures = user.RoleInLectures
                    .Where(l => l.Role == Role.Listener)
                    .Select(l => l.LectureId).ToList()
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.IsGlobalAdmin.ToString()) 
                }),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            tokenDto.Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return tokenDto;
        }

    }
}
