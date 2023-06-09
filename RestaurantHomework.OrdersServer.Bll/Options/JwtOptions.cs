﻿namespace RestaurantHomework.OrdersServer.Bll.Options;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public int TokenLifetime { get; set; }
}