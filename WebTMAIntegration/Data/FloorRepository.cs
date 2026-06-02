using Microsoft.Data.SqlClient;
using System.Data;
using WebTMAIntegration.Data.Helpers;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models.Entities;

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
                        new SqlParameter("@FloorName", SqlDbType.NVarChar, 100)
                        {
                            Value = string.IsNullOrWhiteSpace(floor.FloorName)
                                ? (object)DBNull.Value
                                : floor.FloorName
                        },

                        new SqlParameter("@BuildingID", SqlDbType.Int)
                        {
                            Value = floor.BuildingId ?? 0
                        },

                        new SqlParameter("@BuildingCode", SqlDbType.NVarChar, 50)
                        {
                            Value = string.IsNullOrWhiteSpace(floor.BuildingCode)
                                ? (object)DBNull.Value
                                : floor.BuildingCode
                        },

                        new SqlParameter("@FloorCode", SqlDbType.NVarChar, 50)
                        {
                            Value = string.IsNullOrWhiteSpace(floor.FloorCode)
                                ? (object)DBNull.Value
                                : floor.FloorCode
                        },

                        new SqlParameter("@ImagePath", SqlDbType.NVarChar, 500)
                        {
                            Value = DBNull.Value
                        },

                        new SqlParameter("@FileName", SqlDbType.NVarChar, 100)
                        {
                            Value = DBNull.Value
                        },

                        new SqlParameter("@CreatedBy", SqlDbType.Int)
                        {
                            Value = floor.CreatedBy ?? 0
                        },

                        new SqlParameter("@Alias", SqlDbType.NVarChar, 10)
                        {
                            Value = string.IsNullOrWhiteSpace(floor.Alias)
                                ? (object)DBNull.Value
                                : floor.Alias
                        },

                        new SqlParameter("@FloorTypeId", SqlDbType.Int)
                        {
                            Value = floor.FloorTypeId ?? 1
                        },

                        new SqlParameter("@EffectiveDate", SqlDbType.DateTime)
                        {
                            Value = floor.CreateDate ?? (object)DBNull.Value
                        },

                        new SqlParameter("@IsActive", SqlDbType.Bit)
                        {
                            Value = floor.IsActive ?? false
                        },

                        new SqlParameter("@newId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        },

                        new SqlParameter("@Sequence", SqlDbType.NVarChar, 10)
                        {
                            Value = floor.Sequence?.ToString() ?? (object)DBNull.Value
                        },

                        new SqlParameter("@IsSyncRequired", SqlDbType.Bit)
                        {
                            Value = floor.IsSyncRequired ?? (object)DBNull.Value
                        }
                    };

                    int rows = await _db.ExecuteNonQueryAsync(
                        query: "Insert_Floor",
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

    }
}
