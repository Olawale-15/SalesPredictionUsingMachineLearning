using SalesPrediction.Constant;
using SalesPrediction.Entities;
using SalesPrediction.Interface.IRepository;
using SalesPrediction.Interface.IService;
using SalesPrediction.Response;

namespace SalesPrediction.Service
{
    public class TraderService : ITraderService
    {
        private readonly ITraderRepository _traderRepository;
        private readonly IRoleRepository _roleRepository;

        public TraderService(ITraderRepository traderRepository, IRoleRepository roleRepository)
        {
            _traderRepository = traderRepository;
            _roleRepository = roleRepository;
        }

        public BaseResponse CreateTrader(Trader trader)
        {
         
            var createTrader = new Trader
            {
                FirstName = trader.FirstName,
                LastName = trader.LastName,
                Email = trader.Email,
                PhoneNumber = trader.PhoneNumber,
                City = trader.City,
                State = trader.State,
                Country = trader.Country,
                ZipCode = trader.ZipCode,
                Password = trader.Password
            };
            _traderRepository.CreateTrader(createTrader);
            //var getRole = _roleRepository.GetRoleName(RoleConstant.Trader);
            //if(getRole == null)
            //{
            //    return new BaseResponse
            //    {
            //        Message = "Role not found",
            //        Status = false
            //    };
            //}

            //var role = new Role
            //{
            //    RoleName = getRole.RoleName
            //};

            //_roleRepository.CreateRole(role);
            return new BaseResponse
            {
                Message = "Registration successful",
                Status = true
            };
        }

        //public BaseResponse DeleteTrader(int id)
        //{
        //    var deleteTrader = _traderRepository.GetTraderById(id);
        //    if(deleteTrader == null)
        //    {
        //        return new BaseResponse
        //        {
        //            Message = $"Trader with {id} not found",
        //            Status = false
        //        };
        //    }

        //    _traderRepository.DeleteTrader(deleteTrader);
        //    return new BaseResponse
        //    {
        //        Message = "Trader deleted successfully",
        //        Status = true
        //    };
        //}

        public BaseResponse<ICollection<Trader>> GetAllTrader()
        {
            var getAllTrader = _traderRepository.GetAllTraders();
            if(getAllTrader == null)
            {
                return new BaseResponse<ICollection<Trader>>
                {
                    Message = "Traders not found",
                    Status = false
                };
            }

            var allTraders = getAllTrader.Select(x => new Trader
            {
               // Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                City = x.City,
                State = x.State,
                Country = x.Country,
                ZipCode = x.ZipCode
            }).ToList();

            return new BaseResponse<ICollection<Trader>>
            {
                Data = allTraders,
                Message = "Traders Retrieve Successfully",
                Status = true
            };
        }

//        public BaseResponse<Trader> GetTraderById(int id)
//        {
//            var getTraderDetails = _traderRepository.GetTraderById(id);
//            if(getTraderDetails == null)
//            {
//                return new BaseResponse<Trader>
//                {
//                    Message = "Trader information not found",
//                    Status = false
//                };
//            }

//            var traderDetails = new Trader
//            {
//                //Id = getTraderDetails.Id,
//                FirstName = getTraderDetails.FirstName,
//                LastName = getTraderDetails.LastName,
//                Email = getTraderDetails.Email,
//                PhoneNumber = getTraderDetails.PhoneNumber,
//                City = getTraderDetails.City,
//                State = getTraderDetails.State,
//                Country = getTraderDetails.Country,
//                ZipCode = getTraderDetails.ZipCode
//            };

//            return new BaseResponse<Trader>
//            {
//                Data = traderDetails,
//                Message = "Trade information retrieved successfully",
//                Status = true
//,
//            };
//        }

        public BaseResponse UpdateTrader(int id, Trader trader)
        {
            throw new NotImplementedException();
        }
    }
}
