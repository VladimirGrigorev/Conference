using AutoMapper;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using ConfService.Interface;

namespace ConfService.Service
{
    public class MessageService : IMessageService
    {
        protected readonly IMessageRepository _messageRepository;
        private readonly IApplicationRepository _applicationRepository;
        protected readonly IMapper _mapper;

        public MessageService(IMessageRepository repositoy,
            IApplicationRepository applicationRepository,
            IMapper mapper)
        {
            _messageRepository = repositoy;
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }
        /*public MessageDto Get(int id)
        {
            return _mapper.Map<MessageDto>(_messageRepository.Get(id));
        }

        public IEnumerable<MessageDto> GetAll()
        {
            return _mapper.Map<IEnumerable<MessageDto>>(_messageRepository.GetAll());
        }*/

        public int Add(MessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            message.User = null;

            foreach (var expertId in _messageRepository.GetExpertIds())
            {
                message.MessageNotifications.Add(new MessageNotification()
                {
                    UserId = expertId
                });
            }

            return _messageRepository.Add(message);
        }
        
        public IEnumerable<MessageDto> GetAllByApplicationId(int applicationId, int userId)
        {
            return _mapper.Map<IEnumerable<MessageDto>>(_messageRepository
                .GetAllByApplicationId(applicationId, userId));
        }

        public IEnumerable<MessageDto> GetAllByLectureId(int lectureId, int userId)
        {
            return _mapper.Map<IEnumerable<MessageDto>>(_messageRepository
                .GetAllByLectureId(lectureId , userId));
        }

        public void RemoveMessages(int appId)
        {
            _applicationRepository.RemoveMessageNotifications(appId);
        }
    }
}
