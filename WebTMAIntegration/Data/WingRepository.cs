using Microsoft.Data.SqlClient;
using System.Data;
using WebTMAIntegration.Data.Helpers;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models.Entities;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Data
{
    public class WingRepository : IWingRepository
    {
        private readonly SqlClientHelper _db;

        public WingRepository(SqlClientHelper db)
        {
            _db = db;
        }
        public async Task<int> SaveWingsAsync(List<WingEntity> wings)
        {
            if (wings == null || !wings.Any())
                return 0;

            int totalRowsAffected = 0;

            await using SqlConnection conn =
                await _db.GetOpenConnectionAsync();

            await using SqlTransaction transaction =
                (SqlTransaction)await conn.BeginTransactionAsync();

            try
            {
                foreach (var wing in wings)
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@WingId", SqlDbType.Int)
                        {
                            Value = wing.WingId == 0
                                ? (object)DBNull.Value
                                : wing.WingId
                        },

                        new SqlParameter("@FloorId", SqlDbType.Int)
                        {
                            Value = wing.FloorId ?? 1
                        },

                        new SqlParameter("@WingName", SqlDbType.NVarChar, 200)
                        {
                            Value = string.IsNullOrWhiteSpace(wing.WingName)
                                ? (object)DBNull.Value
                                : wing.WingName
                        },

                        new SqlParameter("@IsActive", SqlDbType.Bit)
                        {
                            Value = wing.IsActive ?? false
                        },

                        new SqlParameter("@WingType", SqlDbType.Int)
                        {
                            Value = wing.WingType ?? 1
                        },

                        new SqlParameter("@Code", SqlDbType.NVarChar, 50)
                        {
                            Value = string.IsNullOrWhiteSpace(wing.Code)
                                ? (object)DBNull.Value
                                : wing.Code
                        },

                        new SqlParameter("@FloorCode", SqlDbType.NVarChar, 50)
                        {
                            Value = string.IsNullOrWhiteSpace(wing.FloorCode)
                                ? (object)DBNull.Value
                                : wing.FloorCode
                        },

                        new SqlParameter("@CreatedBy", SqlDbType.Int)
                        {
                            Value = wing.CreatedBy ?? 1
                        }
                    };

                    int rows = await _db.ExecuteNonQueryAsync(
                        query: "usp_InsertWing",
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

        public async Task<List<WingViewModel>> GetWingsAsync()
        {
            DataTable dt =
                await _db.ExecuteQueryAsync(
                    "usp_GetWings",
                    CommandType.StoredProcedure
                );

            List<WingViewModel> wings =
                new List<WingViewModel>();

            if (dt.Rows.Count == 0)
            {
                return wings;
            }

            foreach (DataRow row in dt.Rows)
            {
                wings.Add(new WingViewModel
                {
                    WingId = row["WingId"] != DBNull.Value
                        ? Convert.ToInt32(row["WingId"]) : 0,

                    SiteName = row["SiteName"]?.ToString(),

                    SiteCode = row["SiteCode"]?.ToString(),

                    BuildingName = row["BuildingName"]?.ToString(),

                    BuildingCode = row["BuildingCode"]?.ToString(),

                    FloorCode = row["FloorCode"]?.ToString(),

                    FloorName = row["FloorName"]?.ToString(),

                    WingName = row["WingName"]?.ToString(),
                    WingType = row["WingType"]?.ToString(),
                    Code = row["Code"]?.ToString(),

                    Address = row["Address"]?.ToString(),

                    IsActive = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"]),

                    CreatedBy = row["CreatedBy"] != DBNull.Value
                        ? Convert.ToInt32(row["CreatedBy"]) : 0,

                    CreateDate = (DateTime)row["CreatedDate"]
                });
            }

            return wings;
        }

    }
}
