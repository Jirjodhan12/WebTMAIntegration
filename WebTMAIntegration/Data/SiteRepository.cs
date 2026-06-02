using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using WebTMAIntegration.Data.Helpers;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models;
using WebTMAIntegration.Models.Entities;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Data
{
    public class SiteRepository : ISiteRepository
    {
        private readonly SqlClientHelper _db;

        public SiteRepository(SqlClientHelper db)
        {
            _db = db;
        }
        public async Task<int> SaveSitesAsync(List<SiteEntity> sites)
        {
            if (sites == null || !sites.Any())
                return 0;

            int totalRowsAffected = 0;

            await using SqlConnection conn =
                await _db.GetOpenConnectionAsync();

            await using SqlTransaction transaction =
                (SqlTransaction)await conn.BeginTransactionAsync();

            try
            {
                foreach (var site in sites)
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@SiteId", SqlDbType.Int)
                        {
                            Value = site.SiteId
                        },

                        new SqlParameter("@SiteTypeId", SqlDbType.Int)
                        {
                            Value = site.SiteTypeId
                        },

                        new SqlParameter("@Name", SqlDbType.NVarChar, 300)
                        {
                            Value = string.IsNullOrWhiteSpace(site.Name)
                                ? (object)DBNull.Value
                                : site.Name
                        },

                        new SqlParameter("@Code", SqlDbType.NVarChar, 50)
                        {
                            Value = site.Code
                        },

                        new SqlParameter("@Description", SqlDbType.NVarChar, 300)
                        {
                            Value = site.Description
                        },


                        new SqlParameter("@IsActive", SqlDbType.Bit)
                        {
                            Value = site.IsActive
                        },

                        new SqlParameter("@IsInternal", SqlDbType.Bit)
                        {
                            Value = site.IsInternal
                        },
                        new SqlParameter("@StateId", SqlDbType.Int)
                        {
                            Value = site.@StateId
                        },
                        new SqlParameter("@CityId", SqlDbType.Int)
                        {
                            Value = site.CityId
                        },
                        new SqlParameter("@RegionsId", SqlDbType.BigInt)
                        {
                            Value = site.RegionsId
                        },
                        new SqlParameter("@Address", SqlDbType.NVarChar, 500)
                        {
                            Value = site.Address
                        },
                        new SqlParameter("@SiteAbbreviation", SqlDbType.NVarChar, 8)
                        {
                            Value = site.SiteAbbreviation
                        },
                        new SqlParameter("@CreatedBy", SqlDbType.BigInt)
                        {
                            Value = site.CreatedBy
                        },
                        new SqlParameter("@IsSyncRequired", SqlDbType.Bit)
                        {
                            Value = site.IsSyncRequired
                        },
                     
                        new SqlParameter("@newId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        }
                    };

                    int rows = await _db.ExecuteNonQueryAsync(
                        query: "Insert_Sites",
                        commandType: CommandType.StoredProcedure,
                        parameters: parameters,
                        connection: conn,
                        transaction: transaction
                    );

                    int insertedId = Convert.ToInt32(
                        parameters.First(p => p.ParameterName == "@newId").Value
                    );

                    totalRowsAffected += rows;
                }

                await transaction.CommitAsync();

                return totalRowsAffected;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<SiteViewModel>> GetSitesAsync()
        {
            DataTable dt =
                await _db.ExecuteQueryAsync(
                    "Get_Sites_Test",
                    CommandType.StoredProcedure
                );

            List<SiteViewModel> buildings = new List<SiteViewModel>();

            if (dt.Rows.Count == 0)
            {
                return buildings;
            }

            foreach (DataRow row in dt.Rows)
            {
                buildings.Add(new SiteViewModel
                {
                    SiteType = "OnSite",

                    Region = "East Region",

                    CampusName = row["Name"]?.ToString(),

                    CampusCode = row["Code"]?.ToString(),

                    Description = row["Description"]?.ToString(),

                    Status = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"])
                });
            }

            return buildings;
        }

    }
}
