using InventaiNovel;
using Inventai.TextAgents;
using Inventai.ImageAgents;
using Microsoft.EntityFrameworkCore;
using web.Server.Data;
using web.Server.Models;

namespace web.Server.Services
{
    public class NovelManagementService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NovelManagementService> _logger;

        public NovelManagementService(IServiceProvider serviceProvider, ILogger<NovelManagementService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<string> GetNovelAsJsonAsync(Guid novelId, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Fetch the novel from the database
            var novel = await dbContext.Novels
                .Include(n => n.Chapters)
                .FirstOrDefaultAsync(n => n.Id == novelId, cancellationToken);

            if (novel == null)
            {
                throw new Exception("Novel not found");
            }

            // Convert the novel to JSON format
            var jsonNovel = JsonSerializer.Serialize(novel);
            return jsonNovel;
        }

        public async Task<string> GenerateImageAsync(Guid novelId, string prompt, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Fetch the novel from the database
            var novel = await dbContext.Novels
                .Include(n => n.Chapters)
                .FirstOrDefaultAsync(n => n.Id == novelId, cancellationToken);

            if (novel == null)
            {
                throw new Exception("Novel not found");
            }

            // Create an image agent and generate the image
            var imageAgent = new ImageAgentStableDiffusion("https://api.segmind.com/v1/stable-diffusion-3.5-turbo-txt2img", "your-segmind-api-key");
            var imageUrl = await imageAgent.GenerateImageAsync(prompt);

            return imageUrl;
        }
            Directory.CreateDirectory(basePath);

            // Save the novel to the specified path
            novelManager.SaveNovel(basePath);

            // Update status to completed
            generation.Status = "Completed";
            await dbContext.SaveChangesAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing generation {GenerationId}", generation.Id);
            generation.Status = "Failed";
            await dbContext.SaveChangesAsync(stoppingToken);
        }

        public async Task<bool> UpdateSceneBackgroundAsync(Guid novelId, string newBackground, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Fetch the novel from the database
            var novel = await dbContext.Novels
                .Include(n => n.Chapters)
                .FirstOrDefaultAsync(n => n.Id == novelId, cancellationToken);

            if (novel == null)
            {
                throw new Exception("Novel not found");
            }

            // Change the background of the scene
            foreach (var chapter in novel.Chapters)
            {
                chapter.Background = newBackground;
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateCharacterImageAsync(Guid novelId, string newImage, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Fetch the novel from the database
            var novel = await dbContext.Novels
                .Include(n => n.Chapters)
                .FirstOrDefaultAsync(n => n.Id == novelId, cancellationToken);

            if (novel == null)
            {
                throw new Exception("Novel not found");
            }

            // Change the character image in the scenes
            foreach (var chapter in novel.Chapters)
            {
                chapter.CharacterImage = newImage;
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateBranchingChoices(Guid novelId, List<string> newChoices, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Fetch the novel from the database
            var novel = await dbContext.Novels
                .Include(n => n.Chapters)
                .FirstOrDefaultAsync(n => n.Id == novelId, cancellationToken);

            if (novel == null)
            {
                throw new Exception("Novel not found");
            }

            // Update the branching choices in the scenes
            foreach (var chapter in novel.Chapters)
            {
                chapter.Choices = newChoices;
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
}