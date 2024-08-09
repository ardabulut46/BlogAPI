using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.AspNetCore.Authorization;
using NuGet.Versioning;
using System.Security.Claims;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Entries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            return await _context.Entries.ToListAsync();
        }

        // GET: api/Entries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entry>> GetEntry(int id)
        {
            var entry = await _context.Entries.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            return entry;
        }

        // PUT: api/Entries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEntry(int id, Entry entry)
        {
            if (id != entry.Id)
            {
                return BadRequest();
            }

            _context.Entry(entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Entries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entry>> PostEntry(Entry entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntry", new { id = entry.Id }, entry);
        }

        // DELETE: api/Entries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEntry(int id)
        {
            var entry = await _context.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntryExists(int id)
        {
            return _context.Entries.Any(e => e.Id == id);
        }
        

        [Authorize]
        [HttpPost("Like or dislike")]
        public async Task<ActionResult> Like(int entryId)
        {
            var entryFind = await _context.Entries.FindAsync(entryId);
            var likeDislikeFind = await _context.UserLikeDislikes.FindAsync(ClaimTypes.NameIdentifier);

            if (entryFind != null)
            {
                if (likeDislikeFind == null)
                {
                    var likeDislike = new UserLikeDislike
                    {
                        MemberId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        EntryId = entryId,
                        Like = true,
                      
                    };
                    entryFind.LikedCount++;
                    await _context.SaveChangesAsync();
                }
                else if (likeDislikeFind != null) 
                {
                    if (likeDislikeFind.Like = true)  
                    {
                        return Ok(new { Message = "You can't like a post twice." });
                    }
                    else
                    {
                        var likeDislike = new UserLikeDislike
                        {
                            MemberId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                            EntryId = entryId,
                            Like = true
                        };
                        entryFind.LikedCount++;
                        entryFind.DislikeCount--;
                        await _context.SaveChangesAsync();
                    }
                }

            }

            return Ok();
        }
        [Authorize]
        [HttpPost("Dislike")]
        public async Task<ActionResult> DisLike(int entryId)
        {
            var entryFind = await _context.Entries.FindAsync(entryId);
            var likeDislikeFind = await _context.UserLikeDislikes.FindAsync(ClaimTypes.NameIdentifier);

            if (entryFind != null)
            {
                if (likeDislikeFind == null)
                {
                    var likeDislike = new UserLikeDislike
                    {
                        EntryId = entryId,
                        Like = false
                    };
                    entryFind.DislikeCount++;
                    await _context.SaveChangesAsync();
                }
                else if (likeDislikeFind != null)
                {
                    if (likeDislikeFind.Like = true)
                    {
                        var likeDislike = new UserLikeDislike
                        {
                            EntryId = entryId,
                            Like = false
                        };
                        entryFind.LikedCount--;
                        entryFind.DislikeCount++;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return Ok(new { Message = "You can't dislike a post twice." });
                    }
                }

            }

            return Ok();
            

        }
        [Authorize]
        [HttpPost("Save the post")]
        public async Task<ActionResult> Save(int entryId) 
        {
            var entryFind = await _context.Entries.FindAsync(entryId);
                
            var bookmarkFind = await _context.Bookmarks.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier),entryId);

            if (bookmarkFind == null)
            {

                var bookmark = new Bookmark
                {
                    EntryId = entryId,
                    MemberId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Bookmarks.Add(bookmark);

            }
            return Ok();

        }
        [Authorize]
        [HttpPost("Remove bookmark")]
        public async Task<ActionResult> RemoveBookmark(int entryId) 
        {
            var entryFind = await _context.Entries.FindAsync(entryId);

            var bookmarkFind = await _context.Bookmarks.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), entryId);

            if (bookmarkFind != null)
            {
                _context.Bookmarks.Remove(bookmarkFind);

            }
            return Ok();

        }



    }
}
