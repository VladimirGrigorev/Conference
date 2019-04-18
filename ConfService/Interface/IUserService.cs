using System;
using System.Collections.Generic;
using System.Text;
using ConfService.Dto;

namespace ConfService.Interface
{
    public interface IUserService
    {
        UserDto Get(int id);
        UserInfoDto GetUserByEmail(string email);
        //IEnumerable<UserDto> GetAll();
        int Add(UserDto userDto);
        TokenDto Authenticate(UserAuthDto userDto);
    }
}
