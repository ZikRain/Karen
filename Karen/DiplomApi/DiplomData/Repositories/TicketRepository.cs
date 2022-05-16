using Dapper;
using DiplomData.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomData.Repositories
{
    public class TicketRepository : BaseRepository
    {
        public TicketRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<Ticket> Create(Ticket ticket)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "insert into tickets set ticket_user_id = @ticket_user_id, ticket_raw = @ticket_raw, ticket_creation_date = @ticket_creation_date; select * from tickets where ticket_id = LAST_INSERT_ID();",
                ticket
                ))
                {
                    return items.ReadFirstOrDefault<Ticket>();
                }
            }
        }

        public async Task<bool> Delete(string ticket_raw)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                return await SqlMapper.ExecuteAsync(_db_connection,
                "delete from tickets where ticket_raw = @ticket_raw;",
                new { ticket_raw }
                ) > 0;
            }
        }
    }
}
