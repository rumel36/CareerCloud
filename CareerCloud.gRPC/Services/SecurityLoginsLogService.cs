using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.SecurityLoginsLog;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLogBase
    {
        
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogService()
        {
            _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
        }

        public override Task<SecurityLoginsLogPayLoad> ReadSecurityLoginsLog(IdRequestLoginsLog request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsLogPayLoad>(() => new SecurityLoginsLogPayLoad()
            {

                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                SourceIP = poco.SourceIP,
                LogonDate = Timestamp.FromDateTime((DateTime)poco.LogonDate),
                IsSuccesful = poco.IsSuccesful
         
            });
        }
        public override Task<Empty> CreateSecurityLoginsLog(SecurityLoginsLogPayLoad request, ServerCallContext context)
        {
            SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[] { new SecurityLoginsLogPoco(){
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = request.LogonDate.ToDateTime(),
                IsSuccesful = request.IsSuccesful
                }
             };
            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogPayLoad request, ServerCallContext context)
        {
            SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[] { new SecurityLoginsLogPoco(){
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = request.LogonDate.ToDateTime(),
                IsSuccesful = request.IsSuccesful
                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogPayLoad request, ServerCallContext context)
        {
            SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[] { new SecurityLoginsLogPoco(){
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = request.LogonDate.ToDateTime(),
                IsSuccesful = request.IsSuccesful
                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

    }
}
