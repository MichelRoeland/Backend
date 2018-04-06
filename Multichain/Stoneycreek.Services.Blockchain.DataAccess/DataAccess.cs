using System;
using System.Linq;

using Stoneycreek.Services.Blockchain.DataAccess.DataCom;

namespace Stoneycreek.Services.Blockchain.DataAccess
{
    public class DataAccess
    {
        public Client[] GetClientData(string clientname = null)
        {
            BlockChainEntities bce = new BlockChainEntities();
            Client[] result = new Client[0];

            if (clientname != null)
            {
                result = bce.Client.Where(f => f.Client1 == clientname).ToArray();
            }
            else
            {
                result = bce.Client.ToArray();
            }

            return result;
        }

        public bool SaveClientData(Client client)
        {
            BlockChainEntities bce = new BlockChainEntities();

            if (client.id > 0)
            {
                var items = bce.Client.Where(f => f.id == client.id).ToArray();
                if (items.Any())
                {
                    var item = items.First();
                    item = AutoMapper.Mapper.Map<Client>(client);
                }
            }
            else
            {
                bce.Client.Add(client);
            }

            try
            {
                bce.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                // handle exception
            }

            return true;
        }

        public bool SaveClientKeys(ClientKeypairs client)
        {
            BlockChainEntities bce = new BlockChainEntities();
            if (client.id > 0)
            {
                var items = bce.ClientKeypairs.Where(f => f.id == client.id).ToArray();
                if (items.Any())
                {
                    var item = items.First();
                    item = AutoMapper.Mapper.Map<ClientKeypairs>(client);
                }
            }
            else
            {
                bce.ClientKeypairs.Add(client);
            }

            try
            {
                bce.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                // handle exception
            }

            return true;
        }

        public bool SaveCommunicationSubscription(CommunicationSubscription client)
        {
            BlockChainEntities bce = new BlockChainEntities();
            if (client.id > 0)
            {
                var items = bce.CommunicationSubscription.Where(f => f.id == client.id).ToArray();
                if (items.Any())
                {
                    var item = items.First();
                    item = AutoMapper.Mapper.Map<CommunicationSubscription>(client);
                }
            }
            else
            {
                bce.CommunicationSubscription.Add(client);
            }

            try
            {
                bce.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                // handle exception
            }

            return true;
        }
        public bool SaveMessages(Messages client)
        {
            BlockChainEntities bce = new BlockChainEntities();
            if (client.id > 0)
            {
                var items = bce.Messages.Where(f => f.id == client.id).ToArray();
                if (items.Any())
                {
                    var item = items.First();
                    item = AutoMapper.Mapper.Map<Messages>(client);
                }
            }
            else
            {
                bce.Messages.Add(client);
            }

            try
            {
                bce.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                // handle exception
            }

            return true;
        }

        public bool SaveStream(Streams client)
        {
            BlockChainEntities bce = new BlockChainEntities();

            if (client.id > 0)
            {
                var items = bce.Streams.Where(f => f.id == client.id).ToArray();
                if (items.Any())
                {
                    var item = items.First();
                    item = AutoMapper.Mapper.Map<Streams>(client);
                }
            }
            else
            {
                bce.Streams.Add(client);
            }

            try
            {
                bce.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                // handle exception
            }

            return true;
        }

        public Streams[] GetStreamsData(string Streamname = null)
        {
            BlockChainEntities bce = new BlockChainEntities();
            Streams[] result = new Streams[0];

            if (Streamname != null)
            {
                result = bce.Streams.Where(f => f.Streamname == Streamname).ToArray();
            }
            else
            {
                result = bce.Streams.ToArray();
            }

            return result;
        }
    }
}
