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
[Route("Card")]
public class CardController(ISender sender) : Controller
{
    [HttpPost(Name = "CreateCard")]
    public async Task<ActionResult<int>> CreateCard([FromBody]CreateCardInputDto cardInput)
    {
        var result = await sender.Send(new CreateCardCommand
        {
            Name = cardInput.Name,
            Front = cardInput.Front,
            Back = cardInput.Back,
            DeckId = cardInput.DeckId,
        });

        return CreatedAtAction(nameof(CreateCard), result);
    }
    
    [HttpGet("{cardId:int}")]
    public async Task<ActionResult<CardOutputDto>> GetCardById(int cardId)
    {
        var deck = await sender.Send(new GetCardByIdQuery
        {
            CardId = cardId
        });

        var result = new CardOutputDto
        {
            Id = deck.Id,
            Name = deck.Name,
            Front = deck.Front,
            Back = deck.Back,
            DeckId = deck.DeckId
        };
        return Ok(result);
    }
    
    [HttpDelete("{cardId:int}")]
    public async Task<ActionResult<int>> DeleteCard(int cardId)
    {
        var result = await sender.Send(new DeleteCardCommand
        {
            CardId = cardId
        });
        return Ok(result);
    }

    [HttpPut("{cardId:int}"), ]
    public async Task<ActionResult> ModifyCard(int cardId, [FromBody]EditCardInputDto newCard)
    {
        await sender.Send(new EditCardCommand
        {
            CardId = cardId,
            NewCardData = new EditCardDTO
            {
                Name = newCard.Name,
                Front = newCard.Front,
                Back = newCard.Back
            },
        });
        
        return Ok();
    }
}