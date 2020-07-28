using AutoMapper;
using ControleDeErrosCodenation.API.DTOs;
using ControleDeErrosCodenation.Domain.Models;
using ControleDeErrosCodenation.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Environment = ControleDeErrosCodenation.Domain.Models.Environment;

namespace ControleDeErrosCodenation.API
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Log, LogDTO>().ForMember(x => x.Environment, y => y.MapFrom<EnvironmentResolver>())
                .ForMember(x => x.Level, y => y.MapFrom<LevelResolver>());
            CreateMap<LogDTO, Log>().ForMember(x => x.IdEnvironment, y => y.MapFrom<EnvironmentReverseResolver>())
                .ForMember(x => x.Environment, y => y.Ignore())
                .ForMember(x => x.IdLevel, y => y.MapFrom<LevelReverseResolver>())
                .ForMember(x => x.Level, y => y.Ignore());
            CreateMap<Level, LevelDTO>().ReverseMap();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>().ForMember(x => x.Role, y => y.MapFrom<UserResolver>());
            CreateMap<Environment, EnvironmentDTO>().ReverseMap();
           
        }
    }

    public class EnvironmentResolver : IValueResolver<Log, LogDTO, string>
    {
        private IEnvironmentRepository _repo;

        public EnvironmentResolver(IEnvironmentRepository repo)
        {
            _repo = repo;
        }

        public string Resolve(Log source, LogDTO destination, string destMember, ResolutionContext context)
        {
            return _repo.SelecionarPorId(source.IdEnvironment).Name;
        }
    }

    public class EnvironmentReverseResolver : IValueResolver<LogDTO, Log, int>
    {
        private IEnvironmentRepository _repo;

        public EnvironmentReverseResolver(IEnvironmentRepository repo)
        {
            _repo = repo;
        }

        public int Resolve(LogDTO source, Log destination, int destMember, ResolutionContext context)
        {
            var envFound = _repo.SelecionarPorNome(source.Environment);
            if (envFound == null)
                return -1;
            return envFound.Id;
        }
    }

    public class LevelResolver : IValueResolver<Log, LogDTO, string>
    {
        private ILevelRepository _repo;

        public LevelResolver(ILevelRepository repo)
        {
            _repo = repo;
        }

        public string Resolve(Log source, LogDTO destination, string destMember, ResolutionContext context)
        {
            return _repo.SelecionarPorId(source.IdLevel).Name;
        }
    }

    public class LevelReverseResolver : IValueResolver<LogDTO, Log, int>
    {
        private ILevelRepository _repo;

        public LevelReverseResolver(ILevelRepository repo)
        {
            _repo = repo;
        }

        public int Resolve(LogDTO source, Log destination, int destMember, ResolutionContext context)
        {
            var levelFound = _repo.SelecionarPorNome(source.Level);
            if (levelFound == null)
                return -1;
            return levelFound.Id;
        }
    }

    public class UserResolver : IValueResolver<UserDTO, User, string>
    {
        private IUserRepository _repo;

        public UserResolver(IUserRepository repo)
        {
            _repo = repo;
        }

        public string Resolve(UserDTO source, User destination, string destMember, ResolutionContext context)
        {
            var userFound = _repo.SelecionarPorId(source.Id);
            if (userFound == null)
                return null;
            return userFound.Role;
        }
    }
}
