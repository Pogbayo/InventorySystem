
using InventorySystem.API.Common;
using InventorySystem.Application.Filter.AuditLogFilter;
using InventorySystem.Application.Filter.PagedResult;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditController : BaseController
    {
        private readonly IAuditLogService _auditService;
        public AuditController(IAuditLogService auditService)
        {
            _auditService = auditService;
        }

        [HttpGet("get-logs-by-filter")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<PagedResult<AuditLog>>>> GetPagedAudits([FromQuery]AuditLogFilter filter)
        {
            var logs = await _auditService.GetPagedAuditLogsAsync(filter)!;
            if (logs == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(logs, "Logs retireved successfully.");
        }
    }
}
