﻿using FoSouzaDev.Customers.Application.Mappings;
using FoSouzaDev.Customers.Domain.Entities;

namespace FoSouzaDev.Customers.Application.DataTransferObjects;

public sealed record AddCustomerDto
{
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required DateTime BirthDate { get; init; }
    public required string Email { get; init; }
    public string? Notes { get; init; }

    public static explicit operator Customer(AddCustomerDto addCustomerDto) =>
        CustomerFactory.AddCustomerDtoToCustomer(addCustomerDto);
}