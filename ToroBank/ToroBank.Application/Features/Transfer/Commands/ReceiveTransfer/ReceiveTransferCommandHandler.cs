﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Application.Features.Users;

namespace ToroBank.Application.Features.Transfer.Commands.ReceiveTransfer
{
    public class ReceiveTransferCommandHandler : IRequestHandler<ReceiveTransferCommand, Result<int>>
    {
        private readonly IUserRepository _userRepository;


        public ReceiveTransferCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<int>> Handle(ReceiveTransferCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByCPFAsync(request?.Origin?.CPF);

            if (user == null)
                throw new System.NullReferenceException("O CPF informado não consta em nossos registros!");

            user.Balance += request.Amount;


            await _userRepository.UpdateAsync(user);
            return Result<int>.Ok(user.Id);
        }
    }
}
