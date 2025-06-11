using AutoMapper;
using CdService.DTOs;
using CdService.Models;
using CdService.Repository;

namespace CdService.Services
{
    public class RecordService : IRecordService
    {
        private IRecordRepository<Record> _recordRepository;
        private IMapper _mapper;
        public List<string> Errors { get; }
        private readonly IFileManagerService _fileManagerService;

        public RecordService(IRecordRepository<Record> recordRepository,
            IMapper mapper,
            IFileManagerService fileManagerService)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
            Errors = new List<string>();
            _fileManagerService = fileManagerService;
        }


        public async Task<IEnumerable<RecordDTO>> GetService()
        {
            var records = await _recordRepository.GetRecordRepository();

            return records.Select(record => _mapper.Map<RecordDTO>(record));
        }


        public async Task<RecordDTO> GetByIdService(int id)
        {
            var record = await _recordRepository.GetByIdRepository(id);

            if (record != null)
            {
                var recordDTO = _mapper.Map<RecordDTO>(record);
                return recordDTO;
            }

            return null;
        }


        public async Task<Record> GetRecordByIdRecordService(int id)
        {
            return await _recordRepository.GetByIdAsyncRecordRepository(id);
        }

        public async Task<IEnumerable<RecordDTO>> GetSortedByTitleRecordService(bool ascending)
        {
            var records = await _recordRepository.GetSortedByTitleRecordRepository(ascending);

            return records.Select(record => _mapper.Map<RecordDTO>(record));
        }


        public async Task<IEnumerable<RecordDTO>> SearchByTitleRecordService(string text)
        {
            var records = await _recordRepository.SearchByTitleRecordRepository(text);

            return records.Select(record => _mapper.Map<RecordDTO>(record));
        }


        public async Task<IEnumerable<RecordDTO>> GetByPriceRangeRecordService(decimal min, decimal max)
        {
            var records = await _recordRepository.GetByPriceRangeRecordRepository(min, max);

            return records.Select(record => _mapper.Map<RecordDTO>(record));
        }


        public async Task<RecordDTO> AddService(RecordInsertDTO recordInsertDTO)
        {
            if (!await _recordRepository.GroupExistsRecordRepository(recordInsertDTO.GroupId))
            {
                throw new ArgumentException($"The Group with ID {recordInsertDTO.GroupId} does not exist");
            }

            var record = _mapper.Map<Record>(recordInsertDTO);

            if (recordInsertDTO.Photo is not null)
            {
                record.ImageRecord = await ProcessImageService(recordInsertDTO.Photo);
            }

            await _recordRepository.AddRepository(record);
            await _recordRepository.SaveRepository();

            return _mapper.Map<RecordDTO>(record);
        }


        public async Task<RecordDTO> UpdateService(int id, RecordUpdateDTO recordUpdateDTO)
        {
            var record = await _recordRepository.GetByIdRepository(id);
            if (record is null) return null;

            _mapper.Map(recordUpdateDTO, record);

            if (recordUpdateDTO.Photo is not null)
            {
                record.ImageRecord = await ProcessImageService(recordUpdateDTO.Photo, record.ImageRecord);
            }

            _recordRepository.UpdateRepository(record);
            await _recordRepository.SaveRepository();

            return _mapper.Map<RecordDTO>(record);
        }


        private async Task<string> ProcessImageService(IFormFile photo, string existingImage = null)
        {
            if (!string.IsNullOrWhiteSpace(existingImage))
            {
                await _fileManagerService.DeleteFile(existingImage, "img");
            }

            using var memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);

            var content = memoryStream.ToArray();
            var extension = Path.GetExtension(photo.FileName);
            var contentType = photo.ContentType;

            return await _fileManagerService.SaveFile(content, extension, "img", contentType);
        }


        public async Task UpdateStockRecordService(int id, int amount)
        {
            var record = await _recordRepository.GetByIdRepository(id);

            if (record == null)
            {
                throw new InvalidOperationException($"Record with ID {id} not found");
            }

            if (amount < 0 && Math.Abs(amount) > record.Stock)
            {
                throw new InvalidOperationException("The decrease cannot be greater than the available stock");
            }

            record.Stock += amount;
            await _recordRepository.UpdateStockRecordRepository(record);
        }


        public async Task<RecordDTO> DeleteService(int id)
        {
            var record = await _recordRepository.GetByIdRepository(id);

            if (record != null)
            {
                var recordDTO = _mapper.Map<RecordDTO>(record);

                if (!string.IsNullOrWhiteSpace(record.ImageRecord))
                {
                    await _fileManagerService.DeleteFile(record.ImageRecord, "img");
                }

                _recordRepository.DeleteRepository(record);
                await _recordRepository.SaveRepository();

                return recordDTO;
            }

            return null;
        }

        public async Task UpdateRecordRecordService(Record record)
        {
            await _recordRepository.UpdateAsyncRecordRepository(record);
        }

    }
}
