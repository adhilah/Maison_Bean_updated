//using MaisonBean.Application.Interfaces;

//using MaisonBean.Domain.Entities;

//using MediatR;

//using Microsoft.AspNetCore.Identity;

//namespace MaisonBean.Application.User.Commands;

//public class DeleteUserCommand
//    : IRequest<DeleteUserResult>
//{
//    public int Id { get; set; }
//}

//public class DeleteUserResult
//{
//    public bool Success { get; set; }

//    public string Message { get; set; } =
//        string.Empty;
//}

//public class DeleteUserCommandHandler
//    : IRequestHandler<
//        DeleteUserCommand,
//        DeleteUserResult
//    >
//{
//    private readonly IUserRepository
//        _userRepository;

//    private readonly UserManager<AppUser>
//        _userManager;

//    private readonly IUnitOfWork
//        _unitOfWork;

//    public DeleteUserCommandHandler(

//        IUserRepository userRepository,

//        UserManager<AppUser> userManager,

//        IUnitOfWork unitOfWork
//    )
//    {
//        _userRepository =
//            userRepository;

//        _userManager =
//            userManager;

//        _unitOfWork =
//            unitOfWork;
//    }

//    public async Task<DeleteUserResult>
//        Handle(

//            DeleteUserCommand request,

//            CancellationToken cancellationToken
//        )
//    {
//        var user =
//            await _userRepository
//                .GetUserWithRelationsAsync(
//                    request.Id,
//                    cancellationToken
//                );

//        // USER NOT FOUND

//        if (user == null)
//        {
//            return new DeleteUserResult
//            {
//                Success = false,

//                Message =
//                    "User not found"
//            };
//        }

//        // BLOCK DELETE IF ORDERS EXIST

//        if (user.Orders.Any())
//        {
//            return new DeleteUserResult
//            {
//                Success = false,

//                Message =
//                    "Cannot delete user with existing orders"
//            };
//        }

//        // DELETE USER

//        var result =
//            await _userManager
//                .DeleteAsync(user);

//        if (!result.Succeeded)
//        {
//            return new DeleteUserResult
//            {
//                Success = false,

//                Message =
//                    "Failed to delete user"
//            };
//        }

//        await _unitOfWork
//            .SaveChangesAsync(
//                cancellationToken
//            );

//        return new DeleteUserResult
//        {
//            Success = true,

//            Message =
//                "User deleted successfully"
//        };
//    }
//}