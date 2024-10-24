﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly BlogContext _context;

    public UsersController(BlogContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return user;
    }

   
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        user.CreatedTime = DateTime.UtcNow;
        user.LastUpdated = DateTime.UtcNow;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.Id) return BadRequest();
        user.LastUpdated = DateTime.UtcNow;

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
