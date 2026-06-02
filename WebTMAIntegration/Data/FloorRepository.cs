using Microsoft.Data.SqlClient;
using System.Data;
using WebTMAIntegration.Data.Helpers;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models.Entities;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Data
{
    public class FloorRepository : IFloorRepository
    {
        private readonly SqlClientHelper _db;

        public FloorRepository(SqlClientHelper db)
        {
            _db = db;
        }
        public async Task<int> SaveFloorsAsync(List<FloorEntity> floors)
        {
            if (floors == null || !floors.Any())
                return 0;

            int totalRowsAffected = 0;

            await using SqlConnection conn =
                await _db.GetOpenConnectionAsync();

            await using SqlTransaction transaction =
                (SqlTransaction)await conn.BeginTransactionAsync();

            try
            {
                foreach (var floor in floors)
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@FloorId", SqlDbType.Int)
                        {
                            Value = floor.FloorId
                        },

                        new SqlParameter("@BuildingId", SqlDbType.Int)
                        {
                            Value = floor.BuildingId ?? 0
                        },

                        new SqlParameter("@FloorTypeId", SqlDbType.Int)
                        {
                            Value = floor.FloorTypeId ?? 1
                        },

                        new SqlParameter("@FloorCode", SqlDbType.NVarChar, 50)
                        {
                            Value = string.IsNullOrWhiteSpace(floor.FloorCode)
                                ? (object)DBNull.Value
                                : floor.FloorCode
                        },

                        new SqlParameter("@BuildingCode", SqlDbType.NVarChar, 50)
                        {
                            Value = string.IsNullOrWhiteSpace(floor.BuildingCode)
                                ? (object)DBNull.Value
                                : floor.BuildingCode
                        },

                        new SqlParameter("@Alias", SqlDbType.NVarChar, 10)
                        {
                            Value = string.IsNullOrWhiteSpace(floor.Alias)
                                ? (object)DBNull.Value
                                : floor.Alias
                        },

                        new SqlParameter("@FloorName", SqlDbType.NVarChar, 100)
                        {
                            Value = string.IsNullOrWhiteSpace(floor.FloorName)
                                ? (object)DBNull.Value
                                : floor.FloorName
                        },

                        new SqlParameter("@Sequence", SqlDbType.NVarChar, 10)
                        {
                            Value = 1
                        },

                        new SqlParameter("@IsActive", SqlDbType.Bit)
                        {
                            Value = floor.IsActive ?? false
                        },

                        new SqlParameter("@CreatedBy", SqlDbType.Int)
                        {
                            Value = floor.CreatedBy ?? 0
                        }

                    };

                    int rows = await _db.ExecuteNonQueryAsync(
                        query: "usp_InsertFloor",
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

        public async Task<List<FloorViewModel>> GetFloorsAsync()
        {
            DataTable dt =
                await _db.ExecuteQueryAsync(
                    "usp_GetFloors",
                    CommandType.StoredProcedure
                );

            List<FloorViewModel> floors =
                new List<FloorViewModel>();

            if (dt.Rows.Count == 0)
            {
                return floors;
            }

            foreach (DataRow row in dt.Rows)
            {
                floors.Add(new FloorViewModel
                {
                    FloorId = row["FloorId"] != DBNull.Value
                        ? Convert.ToInt32(row["FloorId"]) : 0,

                    SiteName = row["SiteName"]?.ToString(),

                    SiteCode = row["SiteCode"]?.ToString(),

                    BuildingName = row["BuildingName"]?.ToString(),

                    BuildingCode = row["BuildingCode"]?.ToString(),

                    FloorCode = row["FloorCode"]?.ToString(),

                    FloorName = row["FloorName"]?.ToString(),

                    Address = row["Address"]?.ToString(),

                    IsActive = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"]),

                    CreatedBy = row["CreatedBy"] != DBNull.Value
                        ? Convert.ToInt32(row["CreatedBy"]) : 0,

                    CreateDate = (DateTime)row["CreateDate"]
                });
            }

            return floors;
        }

    }
}
