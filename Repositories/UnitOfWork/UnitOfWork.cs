using BE_SOCIALNETWORK.Database;
using BE_SOCIALNETWORK.Repositories.Interface;
using BE_SOCIALNETWORK.Repositories.IRespositories;

using Microsoft.EntityFrameworkCore;

namespace BE_SOCIALNETWORK.Repositories.Contracts
{
    public class UnitOfWork : IUnitOfWork
    {
        private  Social_NetworkContext _context;
        private readonly CommentRepository commentRepository;
        private readonly FriendRepository friendRepository;
        private readonly LikeRepository likeRepository;
        private readonly MediaCommentRepository mediaCommentRepository;
        private readonly MediaMessageRepository mediaMessageRepository;
        private readonly MediaPostRepository mediaPostRepository;
        private readonly MessageRepository messageRepository;
        private readonly ParticipantRepository participantRepository;
        private readonly PostRepository postRepository;
        private readonly RoomRepository roomRepository;
        private readonly UserRepository userRepository;
        private readonly LikeTypeRepository likeTypeRepository;
        public UnitOfWork(Social_NetworkContext context) 
        {
            this._context = context;
        }
        public ICommentService CommentRepository => commentRepository ?? new CommentRepository(this._context);
        public IFriendRepository FriendRepository => friendRepository ?? new FriendRepository(this._context);
        public ILikeRepository LikeRepository => likeRepository ?? new LikeRepository(this._context);
        public IMediaCommentRepository MediaCommentRepository => mediaCommentRepository ?? new MediaCommentRepository(this._context);
        public IMediaMessageRepository MediaMessageRepository => mediaMessageRepository ?? new MediaMessageRepository(this._context);
        public IMediaPostRepository MediaPostRepository => mediaPostRepository ?? new MediaPostRepository(this._context);
        public IMessageRepository MessageRepository => messageRepository ?? new MessageRepository(this._context);
        public IParticipantRepository ParticipantRepository => participantRepository ?? new ParticipantRepository(this._context);
        public IPostRepository PostRepository => postRepository ?? new PostRepository(this._context);
        public IRoomRepository RoomRepository => roomRepository ?? new RoomRepository(this._context);
        public IUserRepository UserRepository => userRepository ?? new UserRepository(this._context);
        public ILikeTypeRepository LikeTypeRepository => likeTypeRepository ?? new LikeTypeRepository(this._context);

        public void Commit()
            => _context.SaveChanges();
        public async Task CommitAsync()
            => await _context.SaveChangesAsync();
        public void Rollback()
            => _context.Dispose();
        public async Task RollbackAsync()
            => await _context.DisposeAsync();
    }
}
