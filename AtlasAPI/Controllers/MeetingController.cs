using AtlasAPI.Data;
using AtlasAPI.Models;
using atlasApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtlasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingController : ControllerBase
{
    private readonly AppDbContext _context;

    // Constructor Injection
    public MeetingController(AppDbContext context)
    {
        _context = context;
    }

    // CREATE MEETING
    [HttpPost("create")]
    public async Task<IActionResult> CreateMeeting([FromBody] MeetingDto dto)
    {
        try
        {
            var meeting = new Meeting
            {
                TM_MeetingDate = dto.TM_MeetingDate,
                TM_MeetingTime = dto.TM_MeetingTime,
                TM_Location = dto.TM_Location,
                TM_CompanyName = dto.TM_CompanyName,
                TM_City = dto.TM_City,
                TM_Country = dto.TM_Country,
                TM_ContactPerson = dto.TM_ContactPerson,
                TM_OurRepresentative = dto.TM_OurRepresentative,
                TM_RequestedBy = dto.TM_RequestedBy,
                TM_CompanyBrief = dto.TM_CompanyBrief,
                TM_IsWorking = dto.TM_IsWorking,
                TM_IndiaBusiness = dto.TM_IndiaBusiness,
                TM_NepalBusiness = dto.TM_NepalBusiness,
                TM_AgentsCoreStrength = dto.TM_AgentsCoreStrength,
                TM_OwnLCL = dto.TM_OwnLCL,
                TM_MajorTradeLanes = dto.TM_MajorTradeLanes,
                TM_AirPercentage = dto.TM_AirPercentage,
                TM_SeaPercentage = dto.TM_SeaPercentage,

                // TODO: Replace with logged-in user
                TM_CreatedBy = 1,
                TM_CreatedOn = DateTime.Now
            };

            // Add Grid rows
            foreach (var item in dto.MeetingGrids)
            {
                meeting.MeetingGrids.Add(new MeetingGrid
                {
                    TMG_TradeType = item.TMG_TradeType,
                    TMG_TransportMode = item.TMG_TransportMode,
                    TMG_VolumeYear = item.TMG_VolumeYear,
                    TMG_Nomination = item.TMG_Nomination,
                    TMG_Freehand = item.TMG_Freehand
                });
            }

            _context.Meetings.Add(meeting);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Meeting Created Successfully"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET ALL MEETINGS
    [HttpGet]
    public async Task<IActionResult> GetMeetings()
    {
        var data = await _context.Meetings
            .Include(x => x.MeetingGrids)
            .OrderByDescending(x => x.TM_Id)
            .ToListAsync();

        return Ok(data);
    }

    // GET SINGLE MEETING
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMeeting(int id)
    {
        var data = await _context.Meetings
            .Include(x => x.MeetingGrids)
            .FirstOrDefaultAsync(x => x.TM_Id == id);

        if (data == null)
            return NotFound("Meeting not found");

        return Ok(data);
    }

    // DELETE MEETING
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeeting(int id)
    {
        var meeting = await _context.Meetings
            .Include(x => x.MeetingGrids)
            .FirstOrDefaultAsync(x => x.TM_Id == id);

        if (meeting == null)
            return NotFound("Meeting not found");

        _context.MeetingGrids.RemoveRange(meeting.MeetingGrids);

        _context.Meetings.Remove(meeting);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Deleted Successfully"
        });
    }
}