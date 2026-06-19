using CardLearning.Application.DTO.Input;
using CardLearning.Application.DTO.Output;
using CardLearning.Application.Mediatr.Commands;
using CardLearning.Application.Mediatr.Queries;
using CardLearningAPI.Models.Input;
using CardLearningAPI.Models.Output;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CardLearningAPI.Controllers;

[ApiController]
[Route("Deck")]
public class DeckController(ISender sender) : Controller
{
    [HttpPost(Name = "CreateDeck")]
    public async Task<ActionResult<int>> CreateDeck([FromBody]CreateDeckInputDto deckInputDto)
    {
        var result = await sender.Send(new CreateDeckCommand
        {
            Name = deckInputDto.Name,
            Description = deckInputDto.Description
            
        });

        return CreatedAtAction(nameof(CreateDeck), result);
    }
    
    [HttpGet("{deckId:int}")]
    public async Task<ActionResult<DeckDTO>> GetDeckById(int deckId)
    {
        var deck = await sender.Send(new GetDeckByIdQuery
        {
            Id = deckId,
        });

        var result = new DeckOutputDto
        {
            Id = deck.Id,
            Name = deck.Name,
            Description = deck.Description,
        };
        return Ok(result);
    }
    
    [HttpDelete("{deckId:int}")]
    public async Task<ActionResult<int>> DeleteDeck(int deckId)
    {
        var result = await sender.Send(new DeleteDeckCommand
        {
            DeckId = deckId,
        });
        return Ok(result);
    }

    [HttpPut("{deckId:int}")]
    public async Task<ActionResult> EditDeck(int deckId, [FromBody]EditDeckInputDto newData)
    {
        await sender.Send(new EditDeckDataCommand
        {
            DeckId = deckId,
            NewDeckData = new EditDeckDTO
            {
                Name = newData.Name,
                Description = newData.Description
            }
        });
        
        return Ok();
    }
    
    [HttpGet("{deckId:int}/cards")]
    public async Task<ActionResult<IEnumerable<CardDTO>>> GetDeckCards(int deckId)
    {
        var cards = await sender.Send(new GetDeckCardsQuery
        {
            DeckId = deckId,
        });
        
        return Ok(cards);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeckOutputDto>>> GetDecks()
    {
        var decks = await sender.Send(new GetDecksQuery());
        var result = decks.Select(x => new DeckOutputDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
        });
        
        return Ok(result);
    }
    
    [HttpPut("{deckId:int}/cards")]
    public async Task<ActionResult<IEnumerable<CardOutputDto>>> ModifyDeckCards(int deckId, [FromBody] IEnumerable<int> cardIds)
    {
        await sender.Send(new ModifyDeckCardsCommand
        {
            DeckId = deckId,
            CardIds = cardIds
        });
        
        return Ok();
    }
    
}