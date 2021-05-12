using FluentValidation;

namespace Club.Business.Models.Validation
{
    public class PostValidation : AbstractValidator<Post>
    {
        public PostValidation()
        {
            RuleFor(a => a.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }

    public class PostFeedValidation : AbstractValidator<PostFeed>
    {
        public PostFeedValidation()
        {
            RuleFor(a => a.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
