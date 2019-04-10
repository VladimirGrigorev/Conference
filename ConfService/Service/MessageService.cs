using AutoMapper;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfService.Service
{
    class MessageService
    {
        protected readonly IMessageRepository _messageRepository;
        protected readonly IMapper _mapper;

        public MessageService(IMessageRepository repositoy, IMapper mapper)
        {
            _messageRepository = repositoy;
            _mapper = mapper;
        }
        /*public MessageDto Get(int id)
        {
            return _mapper.Map<MessageDto>(_messageRepository.Get(id));
        }

        public IEnumerable<MessageDto> GetAll()
        {
            return _mapper.Map<IEnumerable<MessageDto>>(_messageRepository.GetAll());
        }

        public int Add(MessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            return _messageRepository.Add(message);
        }*/

        public IEnumerable<MessageDto> GetAllByLectureId(int idLecture)
        {
            return _mapper.Map<IEnumerable<MessageDto>>(_messageRepository.GetWhere(x => x.LectureId==idLecture));
        }
    }
}
