using Microsoft.Data.SqlClient;
using System.Data;
using WebTMAIntegration.Data.Helpers;
using WebTMAIntegration.Data.Interfaces;
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
                    };

                    int rows = await _db.ExecuteNonQueryAsync(
                        query: "usp_InsertSite",
                        commandType: CommandType.StoredProcedure,
                        parameters: parameters,
                        connection: conn,
                        transaction: transaction
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
                    "usp_GetSites",
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
                    SiteId = Convert.ToInt32(row["SiteId"]),

                    RegionsId = Convert.ToInt32(row["RegionsId"]),

                    Name = row["Name"]?.ToString(),

                    Code = row["Code"]?.ToString(),

                    Description = row["Description"]?.ToString(),

                    IsActive = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"]),

                    Address = row["Address"]?.ToString(),

                    CreatedDate = (DateTime)row["CreatedDate"] 

                });
            }

            return buildings;
        }

    }
}
