using BlogAPI.Src.Contextos;
using BlogAPI.Src.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repo.Launch
{
    public class PostRepo : IPost
    {
        #region Attributes

        private readonly BlogPessoalContext _context;

        #endregion Attributes

        #region Constructors

        public PostRepo(BlogPessoalContext context)
        {
            _context = context;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos os temas</para>
        /// </summary>

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p => p.Author).Include(p => p.Theme).ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um post pelo id</para>
        /// </summary>
        /// <param name="id">Id do post</param>

        public async Task<Post> GetPostByIdAsync(int id)
        {
            if (!IdExist(id)) throw new Exception("Id do post não encontrado");
            return await _context.Posts.Include(p => p.Author).Include(p => p.Theme).FirstOrDefaultAsync(p => p.Id == id);

            //função auxiliar
            bool IdExist(int id)
            {
                var aux = _context.Posts.FirstOrDefault(p => p.Id == id);
                return aux != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para criar um post</para>
        /// </summary>
        /// <param name="post">Construtor para post</param>

        public async Task NewPostAsync(Post post)
        {
            if (!AuthorIdExist(post.Author.Id)) throw new Exception("Id do usuário não encontrado");

            if (!ThemeIdExist(post.Theme.Id)) throw new Exception("Id do tema não encontrado");

            await _context.Posts.AddAsync(
                new Post
                {
                    Title = post.Title,
                    Description = post.Description,
                    Photo = post.Photo,
                    Author = _context.Users.FirstOrDefault(p => p.Id == post.Author.Id),
                    Theme = _context.Themes.FirstOrDefault(p => p.Id == post.Theme.Id),

        });
            await _context.SaveChangesAsync();

            //função auxiliar
            bool AuthorIdExist(int id)
            {
                var aux = _context.Users.FirstOrDefault(p => p.Id == post.Author.Id);
                return aux != null;
            }
            bool ThemeIdExist(int id)
            {
                var aux = _context.Themes.FirstOrDefault(p => p.Id == post.Theme.Id);
                return aux != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um post</para>
        /// </summary>
        /// <param name="post">Construtor para atualizar post</param>

        public async Task UpdatePostAsync(Post post)
        {
            if (!ThemeIdExist(post.Theme.Id)) throw new Exception("Id do tema não encontrado");

            var postExist = await GetPostByIdAsync(post.Id);
            postExist.Title = post.Title;
            postExist.Description = post.Description;
            postExist.Photo = post.Photo;
            postExist.Theme = _context.Themes.FirstOrDefault(p => p.Id == post.Theme.Id);

            _context.Posts.Update(postExist);
            await _context.SaveChangesAsync();

            bool ThemeIdExist(int id)
            {
                var aux = _context.Themes.FirstOrDefault(p => p.Id == post.Theme.Id);
                return aux != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um post</para>
        /// </summary>
        /// <param name="id">Id do post</param>

        public async Task DeletePostAsync(int id)
        {
            _context.Posts.Remove(await GetPostByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        #endregion Methods
    }

}
