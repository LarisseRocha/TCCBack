using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sdatcc_v2.Infrastructure;
using Sdatcc_v2.Infrastructure.Entities;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sdatcc_v2.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ArquivoController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private MyDbContext _myDbContext;

		/// <summary>
		/// Salvar o arquivo no caminho indicado, caso ele não exista, ele será criado
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="myDbContext"></param>
		public ArquivoController(IConfiguration configuration, MyDbContext myDbContext)
		{
			_configuration = configuration;

			_myDbContext = myDbContext;


			if (!Directory.Exists("C:\\SdaTcc"))
			{
				Directory.CreateDirectory("C:\\SdaTcc");
			}


		}

		/// <summary>
		/// Realizar a busca de todos os TCCs
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet("BuscarTccs")]
		public async Task<IActionResult> BuscarTccs()
		{
			var arq = _myDbContext.Arquivos;
			return Ok(arq);
		}


		[HttpGet("DownloadAll")]
		public IActionResult DownloadTodosArquivos()
		{
			var arquivos = _myDbContext.Arquivos.ToList();

			string zipFilePath = "C:\\SdaTcc\\all_files.zip";

			using (var zip = new ZipArchive(System.IO.File.Create(zipFilePath), ZipArchiveMode.Create))
			{
				foreach (var arquivo in arquivos)
				{
					string filePath = arquivo.CaminhoArquivo + arquivo.NomeOriginal;
					string entryName = arquivo.NomeOriginal;

					var zipEntry = zip.CreateEntry(entryName);
					using (var zipStream = zipEntry.Open())
					using (var fileStream = new FileStream(filePath, FileMode.Open))
					{
						fileStream.CopyTo(zipStream);
					}
				}
			}

			byte[] zipBytes = System.IO.File.ReadAllBytes(zipFilePath);
			System.IO.File.Delete(zipFilePath);

			return File(zipBytes, "application/force-download", "all_files.zip");
		}


		/// <summary>
		/// Criar um guid para o arquivo
		/// </summary>
		/// <param name="GuidArquivo"></param>
		/// <returns></returns>

		// GET api/<ArquivoController>/5
		[HttpGet("{GuidArquivo}")]
		public IActionResult BuscarTccPorGuid(string GuidArquivo)
		{
			var arquivo = _myDbContext.Arquivos.FirstOrDefault(c => c.GuidArquivo == GuidArquivo);
			if (arquivo == null)
			{
				return NotFound();
			}

			return Ok(arquivo);
		}


		/// <summary>
		/// Carregar os Tccs em PDF
		/// </summary>
		/// <param name="arquivos"></param>
		/// <returns></returns>

		[HttpGet]
		public IActionResult BuscarTodos()
		{
			var arquivos = _myDbContext.Arquivos.ToList();
			if (arquivos.Count == 0)
			{
				return BadRequest();
			}

			return Ok(arquivos);
		}
	
		/// <summary>
		/// Sobe os aqruivos para o servidor
		/// </summary>
		/// <param name="tccId"></param>
		/// <param name="arquivos"></param>
		/// <returns></returns>
		// POST api/<ArquivoController>
		[HttpPost("Upload/{tccId}")]
		public async Task<IActionResult> Upload(int tccId, [FromForm] ICollection<IFormFile> arquivos)
		{
			string[] permittedExtensions = {".pdf" };
			var tccEntity = _myDbContext.Tccs.FirstOrDefault(t => t.Id == tccId);
			if (tccEntity == null)
			{
				return BadRequest();
			}
			string caminhoDestinoArquivo = "C:\\SdaTcc\\";
			foreach (var arquivo in arquivos)
			{
				string guid = Guid.NewGuid().ToString();
				var ext = Path.GetExtension(arquivo.FileName).ToLowerInvariant();

				if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
				{
					return StatusCode(500);
				}
				string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + arquivo.FileName;
				using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
				{
					arquivo.CopyTo(stream);
				}
				ArquivoEntity arquivoEntity = new ArquivoEntity();
				arquivoEntity.GuidArquivo = guid;
				arquivoEntity.CaminhoArquivo = caminhoDestinoArquivo;
				arquivoEntity.NomeOriginal = arquivo.FileName;

				_myDbContext.Arquivos.Add(arquivoEntity);
				tccEntity.Arquivo = arquivoEntity;
			}
			var result = await _myDbContext.SaveChangesAsync();

			return Ok();
		}

		/// <summary>
		/// Baixar o arquivo
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
			[HttpGet("Download/{guid}")]

			public IActionResult DownloadArquivo(string guid)
			{
				var arquivo = _myDbContext.Arquivos.FirstOrDefault(c => c.GuidArquivo == guid);
				
				if (arquivo == null)
				{
					return NotFound(); 
				}
			
				string filePath = arquivo.CaminhoArquivo;
				string fileName = arquivo.NomeOriginal;
				
				byte[] fileBytes = System.IO.File.ReadAllBytes(filePath + "/" + fileName +".pdf");

				return File(fileBytes, "application/force-download", fileName);

			}

		/// <summary>
		/// Deleta um arquivo cadastrado
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		/// /
		[HttpDelete("Delete/{fileName}")]
		public async Task<IActionResult> Delete(string fileName)
		{
			try
			{
				
				ArquivoEntity arquivoToDelete = _myDbContext.Arquivos.FirstOrDefault(a => a.NomeOriginal == fileName);

				if (arquivoToDelete == null)
				{
					return NotFound();
				}

				string caminhoDestinoArquivoOriginal = Path.Combine(arquivoToDelete.CaminhoArquivo, arquivoToDelete.NomeOriginal);
				if (System.IO.File.Exists(caminhoDestinoArquivoOriginal))
				{
					System.IO.File.Delete(caminhoDestinoArquivoOriginal);
				}
				
				_myDbContext.Arquivos.Remove(arquivoToDelete);
				await _myDbContext.SaveChangesAsync();

				return Ok(); 
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message); 
			}
		}

	}
}

