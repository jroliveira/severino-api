namespace Severino.WebApi.Features.Client
{
    using System.ComponentModel.DataAnnotations;

    using Severino.Domain.Client;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Client.Client;

    public class CreateClientModel
    {
        public CreateClientModel(
            string id,
            string name)
        {
            this.Id = id;
            this.Name = name;
        }

        [Required]
        public string Id { get; }

        [Required]
        public string Name { get; }

        public static implicit operator Option<Client>(CreateClientModel model) => NewClient(model.Id, model.Name);
    }
}
