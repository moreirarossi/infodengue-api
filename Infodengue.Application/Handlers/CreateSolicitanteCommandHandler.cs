using AutoMapper;
using Infodengue.Domain.Entities;
using Infodengue.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class CreateSolicitanteCommandHandler : IRequestHandler<CreateSolicitanteCommand, Result<Solicitante>>
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public CreateSolicitanteCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<Solicitante>> Handle(CreateSolicitanteCommand request, CancellationToken cancellationToken)
    {
        var solicitante = await _context.Solicitantes
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.CPF == request.CPF, cancellationToken);

        if (solicitante != null)
        {
            return Result<Solicitante>.Ok(_mapper.Map<Solicitante>(solicitante));
        }

        solicitante = new Solicitante
        {
            Nome = request.Nome,
            CPF = request.CPF
        };

        _context.Solicitantes.Add(solicitante);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Solicitante>.Ok(solicitante);
    }
}
