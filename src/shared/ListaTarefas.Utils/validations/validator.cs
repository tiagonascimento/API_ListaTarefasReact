using FluentValidation;


namespace ListaTarefas.Utils.validations
{
    public static class validator
    {
        public static IRuleBuilderOptions<T, string> validatorIsNullOrEmpt<T>(
        this IRuleBuilder<T, string> ruleBuilder, string msgErro)
        {
            return ruleBuilder
                .NotNull().WithMessage(msgErro)
                .NotEmpty().WithMessage(msgErro);
        }

        public static IRuleBuilderOptions<T, string> validatorSizeMin<T>(
        this IRuleBuilder<T, string> ruleBuilder, int min, string msgErro)
        {
            return ruleBuilder
                .MinimumLength(min).WithMessage(msgErro)
                .NotNull().WithMessage(msgErro);
        }
        public static IRuleBuilderOptions<T, string> validatorSizeMax<T>(
            this IRuleBuilder<T, string> ruleBuilder, int max, string msgErro)
        {
            return ruleBuilder
                .MaximumLength(max).WithMessage(msgErro)
                .NotNull().WithMessage(msgErro);
        }
    }
}
