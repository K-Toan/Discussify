using Grpc.Core;
using Discussify.Protos;
using Discussify.IdentityService.Interfaces;

public class IdentityGrpcService : IdentityService.IdentityServiceBase
{
    private readonly IAppUserRepository _userRepository;

    public IdentityGrpcService(IAppUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override async Task<GetAppUserByIdResponse> GetAppUserById(GetAppUserByIdRequest request, ServerCallContext context)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
        }

        return new GetAppUserByIdResponse
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }
}