using ApiProjeto.Data;
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
    {
        var produtos = await _context.Produtos
            .Include(p => p.Categoria)
            .Select(p => new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                CategoriaId = p.CategoriaId,
                CategoriaNome = p.Categoria!.Nome
            })
            .ToListAsync();

        return Ok(produtos);
    }

    [HttpPost]
    public async Task<ActionResult> Post(ProdutoDTO dto)
    {
        var produto = new Produto
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            CategoriaId = dto.CategoriaId
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, ProdutoDTO dto)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
            return NotFound();

        produto.Nome = dto.Nome;
        produto.Preco = dto.Preco;
        produto.CategoriaId = dto.CategoriaId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
            return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}