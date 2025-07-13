using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using LibraryManagementAPI.Business.Common;
using LibraryManagementAPI.Business.Exceptions;
using LibraryManagementAPI.Business.Utility;
using LibraryManagementAPI.Business.Validators;
using LibraryManagementAPI.Domain.DTOs.Requests;
using LibraryManagementAPI.Domain.DTOs.Responses;
using LibraryManagementAPI.Domain.Model;
using LibraryManagementAPI.Infrastructure.Persistence.Repositories.Interface;
using LibraryManagementCore.Services.Interface;

namespace LibraryManagementAPI.Business.Services.Implementations
{
    public class BookServices(IMapper mapper, IAsyncRepository<Book> _bookRepository) : IBookServices
    {
        /// <summary>
        /// Add a new book
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task<Response<string>> CreateBook(BookDTO bookDto)
        {
            var response = new Response<string>();
            try
            {
                var validate = new CreateBookValidator();
                var validationResult = await validate.ValidateAsync(bookDto);
                if (!validationResult.IsValid)
                    throw new CustomException(string.Join(",", validationResult.Errors));
                var check = await _bookRepository.GetSingleAsync(_=>_.Title == bookDto.Title && _.Author == bookDto.Author);
                if (check != null)
                    throw new CustomException(Constants.alreadyExist);
                
                var book = mapper.Map<Book>(bookDto);
                var result = await _bookRepository.AddAsync(book);
                if (result == null)
                    throw new CustomException(Constants.notCreated);
                response.IsSuccessful = true;
                response.Message = Constants.success;
                response.StatusCode = HttpStatusCode.Created;

            }
            catch (CustomException ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = Constants.notCompleted;
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// Get all Books filtering with search, start and end date and pagination
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Response<IEnumerable<BookResponseDTO>>> GetBooks(GetAllBooksRequest request)
        {
            var response = new Response<IEnumerable<BookResponseDTO>>();
            Expression<Func<Book, bool>> predicate = x => true;
            try
            {
                if (request.Search != null)
                    predicate.And(_ => _.Title.Contains(request.Search) || _.Author.Contains(request.Search)
                                                                        || _.ISBN.Contains(request.Search));
                if (request.StartDate.HasValue)
                    predicate = predicate.And(_ => _.CreatedDate >= request.StartDate); 
                
                if (request.EndDate.HasValue)
                    predicate = predicate.And(_ => _.CreatedDate <= request.EndDate);

                var books = await _bookRepository.GetPagedFilteredAsync(predicate,request.Page, request.PageSize,request.SortColumn, request.SortOrder);
                if (!books.Items.Any())
                    throw new NotFoundException(Constants.notFetched);
                
                var result = mapper.Map<IEnumerable<BookResponseDTO>>(books);
                response.Data = result;
                response.IsSuccessful = true;
                response.Message = Constants.fetched;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (NotFoundException ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
                response.Data = new List<BookResponseDTO>();
                response.StatusCode = HttpStatusCode.NotFound;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = Constants.notCompleted;
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// Update Existing book
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Response<string>> UpdateBook(int Id, BookUpdateDto update)
        {
            var response = new Response<string>();
            try
            {
                var book = await _bookRepository.GetSingleAsync(_ => _.Id == Id);
                if (book == null)
                    throw new NotFoundException(Constants.notFound);

                book.ISBN = update.ISBN;
                book.Author = update.Author;
                book.PublishedDate = update.PublishedDate;
                book.Title = update.Title;
                await _bookRepository.UpdateAsync(book);
                response.IsSuccessful = true;
                response.Message = Constants.updateSuccess;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (NotFoundException ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = Constants.notCompleted;
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// Delete Existing book
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Response<string>> DeleteBook(int Id)
        {
            var response = new Response<string>();
            try
            {
                var book = await _bookRepository.GetSingleAsync(_ => _.Id == Id);
                if (book == null)
                    throw new NotFoundException(Constants.notFound);

                book.IsDeleted = true; 
                await _bookRepository.UpdateAsync(book);
                response.IsSuccessful = true;
                response.Message = Constants.deleteSuccess;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (NotFoundException ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = Constants.notCompleted;
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
