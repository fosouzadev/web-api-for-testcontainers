﻿using FoSouzaDev.Customers.WebApi.Domain.DataTransferObjects;
using FoSouzaDev.Customers.WebApi.Domain.Entities;
using FoSouzaDev.Customers.WebApi.Domain.Exceptions;
using FoSouzaDev.Customers.WebApi.Domain.Repositories;
using FoSouzaDev.Customers.WebApi.Domain.ValueObjects;

namespace FoSouzaDev.Customers.WebApi.Domain.Services;

public sealed class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public async Task<string> AddAsync(AddCustomerDto customer)
    {
        Customer entity = new()
        {
            Id = string.Empty,
            FullName = new FullName(customer.Name, customer.LastName),
            BirthDate = new BirthDate(customer.BirthDate),
            Email = new Email(customer.Email),
            Notes = customer.Notes
        };

        await customerRepository.AddAsync(entity);

        return entity.Id;
    }

    public async Task<Customer?> GetByIdAsync(string id)
    {
        return (await customerRepository.GetByIdAsync(id))
            ?? throw new NotFoundException(id);
    }

    public async Task EditAsync(string id, EditCustomerDto customer)
    {
        Customer? entity = await this.GetByIdAsync(id);

        entity!.FullName = new FullName(customer.Name, customer.LastName);
        entity.Notes = customer.Notes;

        await customerRepository.EditAsync(entity);
    }

    public async Task DeleteAsync(string id)
    {
        _ = await this.GetByIdAsync(id);

        await customerRepository.DeleteAsync(id);
    }
}