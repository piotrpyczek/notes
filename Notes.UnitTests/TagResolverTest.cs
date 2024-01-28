using Notes.Infrastructure.TagResolver;

namespace Notes.UnitTests
{
    public class TagResolverTest
    {
        [Theory]
        [InlineData("test@gmail.com")]
        [InlineData("TeSt@GMAIL.com")]
        [InlineData("test.test@gamil.com")]
        [InlineData("dummy text test@gamil.com end")]
        public void email_positive_verification(string text)
        {
            //Act
            var result = new EmailTagResolver().AppliesTo(text);

            // Assert
            Assert.True(result, text);
        }

        [Theory]
        [InlineData("test@ gamil.com")]
        [InlineData("test. test@gmail")]
        [InlineData("Lorem ipsum dolor sit amet")]
        public void email_negative_verification(string text)
        {
            //Act
            var result = new EmailTagResolver().AppliesTo(text);

            // Assert
            Assert.False(result, text);
        }

        [Theory]
        [InlineData("321654789")]
        [InlineData("(+48)321654789")]
        [InlineData("+48321654789")]
        [InlineData("(+48) 321 654 789")]
        [InlineData("+48 321 654 789")]
        [InlineData("dummy text 321654789 end")]
        [InlineData("dummy text 321 654 789 end")]
        public void phone_positive_verification(string text)
        {
            //Act
            var result = new PhoneTagResolver().AppliesTo(text);

            // Assert
            Assert.True(result, text);
        }

        [Theory]
        [InlineData("32654789")]
        [InlineData("321 65 4 789")]
        [InlineData("326A4789")]
        [InlineData("dummy text 1234 end")]
        public void phone_negative_verification(string text)
        {
            //Act
            var result = new PhoneTagResolver().AppliesTo(text);

            // Assert
            Assert.False(result, text);
        }
    }
}
