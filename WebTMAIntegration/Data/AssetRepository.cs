using Microsoft.Data.SqlClient;
using System.Data;
using WebTMAIntegration.Data.Helpers;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models.Entities;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Data
{
    public class AssetRepository : IAssetRepository
    {
        private readonly SqlClientHelper _db;

        public AssetRepository(SqlClientHelper db)
        {
            _db = db;
        }
        public async Task<int> SaveEquipmentTypesAsync(List<EquipmentTypeEntity> equipments)
        {
            if (equipments == null || !equipments.Any())
                return 0;

            int totalRowsAffected = 0;

            await using SqlConnection conn =
                await _db.GetOpenConnectionAsync();

            await using SqlTransaction transaction =
                (SqlTransaction)await conn.BeginTransactionAsync();

            try
            {
                foreach (var equipment in equipments)
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Id", SqlDbType.Int)
                        {
                            Value = equipment.Id
                        },

                        new SqlParameter("@Code", SqlDbType.NVarChar, 50)
                        {
                            Value = equipment.Code
                        },
  
                        new SqlParameter("@Description", SqlDbType.NVarChar, 300)
                        {
                            Value = string.IsNullOrWhiteSpace(equipment.Description)
                                ? (object)DBNull.Value
                                : equipment.Description
                        }
                    };

                    int rows = await _db.ExecuteNonQueryAsync(
                        query: "usp_InsertEquipmentTypes",
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
        public async Task<int> SaveEquipmentSubTypesAsync(List<EquipmentSubTypeEntity> equipments)
        {
            if (equipments == null || !equipments.Any())
                return 0;

            int totalRowsAffected = 0;

            await using SqlConnection conn =
                await _db.GetOpenConnectionAsync();

            await using SqlTransaction transaction =
                (SqlTransaction)await conn.BeginTransactionAsync();

            try
            {
                foreach (var equipment in equipments)
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Id", SqlDbType.Int)
                        {
                            Value = equipment.Id
                        },

                        new SqlParameter("@Code", SqlDbType.NVarChar, 50)
                        {
                            Value = equipment.Code
                        },
  
                        new SqlParameter("@Description", SqlDbType.NVarChar, 300)
                        {
                            Value = string.IsNullOrWhiteSpace(equipment.Description)
                                ? (object)DBNull.Value
                                : equipment.Description
                        },
                        new SqlParameter("@ParentId", SqlDbType.Int)
                        {
                            Value = equipment.ParentId
                        },
                    };

                    int rows = await _db.ExecuteNonQueryAsync(
                        query: "usp_InsertEquipmentSubTypes",
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

        public async Task<List<SiteViewModel>> GetAssetsAsync()
        {
            DataTable dt =
                await _db.ExecuteQueryAsync(
                    "usp_GetAssets",
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
