using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace SearchService;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly IMapper _mapper;

    public UserCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        Console.WriteLine("---> Consuming user created " + context.Message.Id);
        var item = _mapper.Map<Item>(context.Message);
        await item.SaveAsync();
    }
}
