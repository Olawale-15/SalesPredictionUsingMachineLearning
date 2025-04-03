using SalesPrediction.Entities;
using SalesPrediction.Interface.IJWTSettingsService;
using SalesPrediction.Interface.IRepository;
using SalesPrediction.Interface.IService;
using SalesPrediction.Response;

namespace SalesPrediction.Service
{
    public class TraderLoginService:ITraderLoginService
    {
        private readonly ITraderRepository _traderRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJWTSettings _jWTSettings;

        public TraderLoginService(ITraderRepository traderRepository, IJWTSettings jWTSettings, IRoleRepository roleRepository)
        {
            _traderRepository = traderRepository;
            _jWTSettings = jWTSettings;
            _roleRepository = roleRepository;
        }

        public BaseResponse<TraderResponseModel> Login(TraderLoginModel responseModel)
        {
            var checkEmail = _traderRepository.GetByEmail(responseModel.Email);
            if(checkEmail == null)
            {
                return new BaseResponse<TraderResponseModel>
                {
                    Message = "Invalid credential",
                    Status = false,
                };
            }
            //var password = BCrypt.Net.BCrypt.Verify(responseModel.Password, checkEmail.Password);
            if (responseModel.Password != null)
            {
                var token = _jWTSettings.GenerateToken(checkEmail);
                return new BaseResponse<TraderResponseModel>
                {
                    Message = "Login successful",
                    Status = true,
                    Data = new TraderResponseModel
                    {
                        Email = checkEmail.Email,
                        UserName = $"{checkEmail.FirstName}{checkEmail.LastName}",
                        Token = token,
                       // Roles = checkEmail.Roles.Select(x => x.RoleName).ToList(),
                    }
                };
            }

            return new BaseResponse<TraderResponseModel>
            {
                Message = "Invalid Password",
                Status = false,
            };
        }
    }
}
