using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace SearchService;

public class UserUpdatedConsumer : IConsumer<UserUpdated>
{
    private readonly IMapper _mapper;

    public UserUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<UserUpdated> context)
    {
        Console.WriteLine("---> Consuming user created " + context.Message.Id);
        var item = _mapper.Map<Item>(context.Message);
        var result = await DB.Update<Item>().Match(a => a.ID == context.Message.Id)
        .ModifyOnly(x => new 
        {
            x.UserName,
            x.ImageUrl,
        }, item).ExecuteAsync();
        if (!result.IsAcknowledged)
        {
            throw new MessageException(typeof(UserUpdated) , "Problem for Updating MongoDb");
        }


    }
}
