using System;
using FluentAssertions;
using Xunit;

namespace HelperPack.Tests
{

    public class TaskHelperTests
    {
        [Fact]
        public void RunWithRetry_ActionIsExecuted()
        {
            int i = 0;
            int count = 0;

            Action action = () => { i = 1; count++; };

            HelperPack.Retry.RunWithRetry(action, 5);

            i.Should().Be(1);
            count.Should().Be(1);
        }

        [Fact]
        public void RunWithRetry_RetryOnException()
        {
            int i = 0;

            Action action = () => { i++; throw new InvalidOperationException(); };

            bool result = false;
            try
            {
                HelperPack.Retry.RunWithRetry(action, 5);
                result = true;
            }
            catch
            {
            }

            i.Should().Be(5);
            result.Should().BeFalse();
        }

        [Fact]
        public void RunWithRetry_RetryOnExceptionOnce()
        {
            int i = 0;
            bool exception = false;

            Action action = () => { i++; if (i < 2) { exception = true; throw new InvalidOperationException(); } };

            HelperPack.Retry.RunWithRetry(action, 5);
            i.Should().Be(2);
            exception.Should().BeTrue();

        }

        [Fact]
        public void RunWithRetry_RetryOnExceptionOnceWithErrorAction()
        {
            int i = 0;
            bool exception = false;
            Exception x = null;
            int count = -1;

            Action action = () => { i++; if (i < 2) { exception = true; throw new InvalidOperationException(); } };
            Action<Exception, int> error = (e, c) => { x = e; count = c; };

            HelperPack.Retry.RunWithRetry(action, 5, error);
            i.Should().Be(2);
            count.Should().Be(1);
            exception.Should().BeTrue();

        }

        [Fact]
        public void RunWithRetryFunction_RetryOnExceptionOnceWithErrorAction()
        {
            int i = 0;
            bool exception = false;
            Exception x = null;
            int count = -1;

            Func<int> action = () => { i++; if (i < 2) { exception = true; throw new InvalidOperationException(); } else return 10; };
            Action<Exception, int> error = (e, c) => { x = e; count = c; };

            var result = HelperPack.Retry.RunWithRetry<Exception, int>(action, 5, error);

            result.Should().Be(10);

            i.Should().Be(2);
            count.Should().Be(1);
            exception.Should().BeTrue();

        }
    }
}
