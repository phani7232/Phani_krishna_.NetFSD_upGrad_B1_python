using AutoMapper;
using ELearningAPI.Data;
using ELearningAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ResultsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetResultsByUser(int userId)
    {
        var results = await _context.Results
            .Where(r => r.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        if (!results.Any()) return NotFound();

        return Ok(_mapper.Map<IEnumerable<ResultDto>>(results));
    }
}