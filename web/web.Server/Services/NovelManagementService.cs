using InventaiNovel;
using Inventai.TextAgents;
using Inventai.ImageAgents;
using Microsoft.EntityFrameworkCore;
using web.Server.Data;
using web.Server.Models;

using System.Text.Json;

namespace web.Server.Services
{
    public class NovelManagementService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NovelManagementService> _logger;
        private readonly InventaiNovelManager _novelManager;

        public NovelManagementService(IServiceProvider serviceProvider, ILogger<NovelManagementService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            TextAgentOpenAI textAgentOpenAI = new TextAgentOpenAI("https://api.openai.com/v1/chat/completions", "your-openai-api-key");
            ImageAgentStableDiffusion imageAgentStableDiffusion = new ImageAgentStableDiffusion("https://api.segmind.com/v1/stable-diffusion-3.5-turbo-txt2img", "your-segmind-api-key");
            _novelManager = new InventaiNovelManager(textAgentOpenAI, imageAgentStableDiffusion);
        }

        public async Task<string> GetNovelPath(Guid novelId)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            // Fetch the novel from the database
            var novel = await dbContext.NovelGenerations
                .FirstOrDefaultAsync(n => n.Id == novelId);
            if (novel == null)
            {
                throw new Exception("Novel not found");
            }
            return Path.Combine("Novels", $"{novel.Id}.json");
        }

        public async Task<string> GetNovelAsJsonAsync(Guid novelId, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Fetch the novel from the database
            var novel = _novelManager.LoadNovel(await GetNovelPath(novelId));

            if (novel == null)
            {
                throw new Exception("Novel not found");
            }

            // Convert the novel to JSON format
            var jsonNovel = JsonSerializer.Serialize(novel);
            return jsonNovel;
        }

        public async Task<byte[]> GenerateImageAsync(string prompt, CancellationToken cancellationToken)
        {
            // Create an image agent and generate the image
            var imageAgent = new ImageAgentStableDiffusion("https://api.segmind.com/v1/stable-diffusion-3.5-turbo-txt2img", "your-segmind-api-key");
            var imageData = await imageAgent.GenerateImageAsync(new Inventai.Core.ImageRequest
            {
                Prompt = prompt,
                NegativePrompt = "bad quality",
                Width = 512,
                Height = 512,
                Steps = 50,
            });

            return imageData;
        }

        public async Task<bool> UpdateSceneBackgroundAsync(Guid novelId, byte[] newBackground, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            // Fetch the novel from the database
            var novel = _novelManager.LoadNovel(await GetNovelPath(novelId));

            if (novel == null)
            {
                throw new Exception("Novel not found");
            }

            // Change the background of the scene
            foreach (var chapter in novel.Scenes)
            {
                chapter.BackgroundImage = newBackground;
            }

            return true;
        }

        public async Task<bool> UpdateCharacterImageAsync(Guid novelId, string newImage, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            // Fetch the novel from the database
            var novel = _novelManager.LoadNovel(await GetNovelPath(novelId));

            if (novel == null)
            {
                throw new Exception("Novel not found");
            }

            //// Change the character image in the scenes
            //foreach (var chapter in novel.Scenes)
            //{
            //    chapter.CharacterImage = newImage;
            //}

            //await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateBranchingChoices(Guid novelId, List<string> newChoices, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            // Fetch the novel from the database
            var novel = _novelManager.LoadNovel(await GetNovelPath(novelId));

            if (novel == null)
            {
                throw new Exception("Novel not found");
            }

            //// Update the branching choices in the scenes
            //foreach (var chapter in novel.Scenes)
            //{
            //    chapter.Choices = newChoices;
            //}

            //await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}