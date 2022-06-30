using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {


        private SqlConnection sqlConnection;

        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string AddAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("spAddAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                cmd.Parameters.AddWithValue("@City", addressModel.City);
                cmd.Parameters.AddWithValue("@State", addressModel.State);
                cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                this.sqlConnection.Open();
                cmd.ExecuteScalar();
                return " Address Added Successfully";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public bool DeleteAddress(int AddressId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("spDeleteAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AddressId", AddressId);
                this.sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    

        public bool UpdateAddress(int AddressId, AddressModel addressModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("spUpdateAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AddressId", AddressId);
                cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                cmd.Parameters.AddWithValue("@City", addressModel.City);
                cmd.Parameters.AddWithValue("@State", addressModel.State);
                cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                this.sqlConnection.Open();
                cmd.ExecuteScalar();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
    
}