# BookStore Project

### Keywords: EfCore, AutoMapper, Middleware, Authorization - JWT , JwtBearer , UnitTests , Xunit , Services , FluentValidation , FluentAssertion , Dependency Injection

### Summary

- EfCore kullanılarak DbContext ve Entities oluşturuldu. Book,Genre,Author,User işlemleri Operations klasöründe altında yapıldı.
- Command 'lar ve Query 'ler için Validator classlar oluşturuldu. (FluentValidation)
- İlgili operasyonlar için Controller'lar oluşturuldu ve ihtiyaçlar doğrultusunda HttpMethodları kullanıldı.
- Operasyonlarda kullanılmak üzere lgili ViewModel ve DTO yapıları oluşturuldu.
- Mapper ile oluşturulan ViewModel'lerin ve DTO'ların daha efektif kullanılması sağlandı. (AutoMapper)
- Dependency injection ile bağımlılıklar en aza indirgendi.
- CustomMiddleware ile Logger Service kullanıldı.
- Controller'lar için Authorization sağlandı. (JwtBearer)
- CreateCommandlar için ilgili UnitTest 'ler olabildiğince izole edilmiş halde yazıldı. (Xunit, FluentAssortions)
