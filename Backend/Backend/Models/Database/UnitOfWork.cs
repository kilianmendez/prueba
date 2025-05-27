using Backend.Models.Database.Repositories;

namespace Backend.Models.Database;

public class UnitOfWork
{
    private readonly DataContext _dataContext;
    private UserRepository _userRepository = null!;
    private RecommendationRepository _recommendationRepository = null!;
    private AccommodationRepository _accommodationRepository = null!;
    private ReservationRepository _reservationRepository = null!;
    private EventRepository _eventRepository = null!;
    private ForumRepository _forumRepository = null!;
    private HostRepository _hostRepository = null!;
    private AdminRepository _adminRepository = null!;

    public UserRepository UserRepository => _userRepository ??= new UserRepository(_dataContext);
    public RecommendationRepository RecommendationRepository => _recommendationRepository ??= new RecommendationRepository(_dataContext);
    public AccommodationRepository AccommodationRepository => _accommodationRepository ??= new AccommodationRepository(_dataContext);
    public ReservationRepository ReservationRepository => _reservationRepository ??= new ReservationRepository(_dataContext);
    public EventRepository EventRepository => _eventRepository ??= new EventRepository(_dataContext);
    public ForumRepository ForumRepository => _forumRepository ??= new ForumRepository(_dataContext);
    public HostRepository HostRepository => _hostRepository ??= new HostRepository(_dataContext);
    public AdminRepository AdminRepository => _adminRepository ??= new AdminRepository(_dataContext);

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<bool> SaveAsync()
    {
        return await _dataContext.SaveChangesAsync() > 0;
    }

}
