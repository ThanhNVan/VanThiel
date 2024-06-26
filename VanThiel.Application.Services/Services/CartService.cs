﻿using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using VanThiel.Application.Services.Base;
using VanThiel.Core.Repositories;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;
using VanThiel.Core.ExceptionClasses;
using System.Linq;
using System.Security.Claims;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Domain.Enums;
using System;
using VanThiel.Domain.DTOs.ReturnModel;

namespace VanThiel.Application.Services;

public class CartService : BaseVanThielService<Cart, ICartRepository>, ICartService
{
    #region [ Fields ]
    private IProductRepository _productRepository;
    private IUserRepository _userRepository;
    #endregion

    #region [ Ctor ]
    public CartService(ILogger<CartService> logger, ICartRepository repository, IUserRepository userRepository, IProductRepository productRepository) : base(logger, repository)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
    }
    #endregion

    #region [ Public Method - Get ]
    public async ValueTask<IEnumerable<CartInfo>> GetMany_ByUserAsync(ClaimsIdentity identity, CancellationToken cancellationToken = default)
    {
        GuardParametter.IsValidIdentity(identity);

        var userId = identity.FindFirst("UserId").Value;
        GuardParametter.IsValidJwtClaim(userId);

        var result = default(IEnumerable<CartInfo>);
        var isExistUser = await this._userRepository.IsExistAsync(userId, cancellationToken);

        if (!isExistUser) {
            throw new NotFoundException("Not found user");
        }

        result = await this._repository.GetMany_ByUserAsync(userId, cancellationToken);

        return result;
    }
    #endregion

    #region [ Public Method - Post ]
    public async ValueTask<bool> Update_AddSingleToCartAsync(ClaimsIdentity identity, string productId, CancellationToken cancellationToken = default)
    {
        GuardParametter.IsValidIdentity(identity);

        var userId = identity.FindFirst("UserId").Value;
        GuardParametter.IsValidJwtClaim(userId);
        GuardParametter.StringIsNullOrEmpty(productId);

        var isExistUser = await this._userRepository.IsExistAsync(userId, cancellationToken);

        var userCart = await this._repository.GetSingle_ByUserAndProductAsync(userId, productId, cancellationToken);

        var instock = await this._productRepository.GetSingle_InstockAsync(productId, cancellationToken);

        if (userCart is null && instock > 0) { 
            var newCart = new Cart() { 
                ProductId = productId,
                Quantity = 1,
                UserId = userId,
                Status = CartStatus.Added,
            };
            await this._repository.AddSingleAsync(newCart, cancellationToken);
            return true;
        }

        if (instock == userCart.Quantity)
        {
            throw new ArgumentException("Surpass the value instock");
        }

        userCart.Quantity++;
        await this._repository.UpdateAsync(userCart, cancellationToken);
        return true;
    }
    
    public async ValueTask<bool> Update_SubtractFromCartAsync(ClaimsIdentity identity, string productId, CancellationToken cancellationToken = default)
    {
        GuardParametter.IsValidIdentity(identity);

        var userId = identity.FindFirst("UserId").Value;
        GuardParametter.IsValidJwtClaim(userId);
        GuardParametter.StringIsNullOrEmpty(productId);

        var isExistUser = await this._userRepository.IsExistAsync(userId, cancellationToken);

        var userCart = await this._repository.GetSingle_ByUserAndProductAsync(userId, productId, cancellationToken);
        
        if (userCart is null) { 
            throw new NotFoundException("Not found cart");
        }

        if (userCart.Quantity == 1)
        {
            userCart.IsActive = false;
            await this._repository.UpdateAsync(userCart, cancellationToken);
            return true;
        }

        userCart.Quantity--;
        await this._repository.UpdateAsync(userCart, cancellationToken);
        return true;
    }

    public async ValueTask<bool> Update_AddManyToCartAsync(ClaimsIdentity identity, UpdateCart model, CancellationToken cancellationToken = default)
    {
        GuardParametter.IsValidIdentity(identity);

        var userId = identity.FindFirst("UserId").Value;
        GuardParametter.IsValidJwtClaim(userId);
        GuardParametter.StringIsNullOrEmpty(model.ProductId);

        var isExistUser = await this._userRepository.IsExistAsync(userId, cancellationToken);

        var userCart = await this._repository.GetSingle_ByUserAndProductAsync(userId, model.ProductId, cancellationToken);
        var instock = await this._productRepository.GetSingle_InstockAsync(model.ProductId, cancellationToken);

        if (userCart is null && instock < model.Quantity)
        {
            var newCart = new Cart()
            {
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                UserId = userId,
                Status = CartStatus.Added,
            };
            await this._repository.AddSingleAsync(newCart, cancellationToken);
            return true;
        }


        var newQuantity = userCart.Quantity + model.Quantity;
        if (instock == userCart.Quantity || instock < newQuantity)
        {
            throw new ArgumentException("Surpass the value instock");
        }

        userCart.Quantity = newQuantity;
        await this._repository.UpdateAsync(userCart, cancellationToken);
        return true;
    }
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    public async ValueTask<bool> Update_RemoveFromCartAsync(ClaimsIdentity identity, string productId, CancellationToken cancellationToken = default)
    {
        GuardParametter.IsValidIdentity(identity);

        var userId = identity.FindFirst("UserId").Value;
        GuardParametter.IsValidJwtClaim(userId);
        GuardParametter.StringIsNullOrEmpty(productId);

        var isExistUser = await this._userRepository.IsExistAsync(userId, cancellationToken);

        var userCart = await this._repository.GetSingle_ByUserAndProductAsync(userId, productId, cancellationToken);
        
        if (userCart is null) { 
            throw new NotFoundException("Not found cart");
        }

        userCart.IsActive = false;
        await this._repository.UpdateAsync(userCart, cancellationToken);
        return true;
    }
    #endregion
}
