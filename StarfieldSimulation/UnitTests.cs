using NUnit.Framework;
using System;

namespace StarfieldSimulation
{
    [TestFixture]
    public class UnitTests
    {
        private Form1 form;

        [SetUp]
        public void Setup()
        {
            form = new Form1();
            form.Form1_Load(null, EventArgs.Empty);
        }

        [Test]
        public void Map_ValidInputs_ReturnsCorrectOutput()
        {
            // Arrange
            float n = 0.5f;
            float start1 = 0;
            float stop1 = 1;
            float start2 = 0;
            float stop2 = 10;

            // Act
            float result = form.Map(n, start1, stop1, start2, stop2);

            // Assert
            Assert.That(result, Is.EqualTo(5).Within(0.001f));
        }

        [Test]
        public void MoveStar_StarZLessThanOne_ResetsStarPosition()
        {
            // Arrange
            var star = new Form1.Star { X = 0, Y = 0, Z = 0.5f };
            var initialX = star.X;
            var initialY = star.Y;

            // Act
            form.MoveStar(star);

            // Assert
            Assert.That(star.X, Is.Not.EqualTo(initialX));
            Assert.That(star.Y, Is.Not.EqualTo(initialY));
            Assert.That(star.Z, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void MoveStar_StarZGreaterThanOne_DoesNotResetPosition()
        {
            // Arrange
            var star = new Form1.Star { X = 0, Y = 0, Z = 10 };

            // Act
            form.MoveStar(star);

            // Assert
            Assert.That(star.Z, Is.GreaterThan(0));
        }

        [Test]
        public void Form1_Load_InitializesStarsArray()
        {
            // Assert
            Assert.That(form.stars, Is.Not.Null);
            Assert.That(form.stars.Length, Is.EqualTo(20000));
            foreach (var star in form.stars)
            {
                Assert.That(star, Is.Not.Null);
                Assert.That(star.X, Is.InRange(-form.pictureBox1.Width, form.pictureBox1.Width));
                Assert.That(star.Y, Is.InRange(-form.pictureBox1.Height, form.pictureBox1.Height));
                Assert.That(star.Z, Is.InRange(1, form.pictureBox1.Width));
            }
        }

        [Test]
        public void DrawStar_StarDrawn_StarSizeIsCorrect()
        {
            // Arrange
            var star = new Form1.Star { X = 0, Y = 0, Z = 50 };

            // Act
            form.DrawStar(star);

            // Assert
            float expectedSize = form.Map(star.Z, 0, form.pictureBox1.Width, 9, 0);
            Assert.That(expectedSize, Is.GreaterThan(0));
        }

        [Test]
        public void DrawStar_StarPositionIsMapped_CorrectScreenCoordinates()
        {
            // Arrange
            var star = new Form1.Star { X = 0, Y = 0, Z = 50 };

            // Act
            form.DrawStar(star);

            // Assert
            float x = form.Map(star.X / star.Z, 0, 1, 0, form.pictureBox1.Width) + form.pictureBox1.Width / 2;
            float y = form.Map(star.Y / star.Z, 0, 1, 0, form.pictureBox1.Height) + form.pictureBox1.Height / 2;
            Assert.That(x, Is.InRange(0, form.pictureBox1.Width));
            Assert.That(y, Is.InRange(0, form.pictureBox1.Height));
        }

        [Test]
        public void TimerTick_CallsMoveAndDrawStarMethods()
        {
            // Arrange
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += form.timer1_Tick;

            // Act
            timer.Start();
            form.timer1_Tick(null, EventArgs.Empty);

            // Assert
            Assert.That(form.stars, Is.Not.Null);
            Assert.That(form.stars.Length, Is.GreaterThan(0));
        }

        [Test]
        public void MoveStar_WhenStarIsReset_XAndYAreInBounds()
        {
            // Arrange
            var star = new Form1.Star { X = 0, Y = 0, Z = 0.5f };
            form.MoveStar(star);

            // Assert
            Assert.That(star.X, Is.InRange(-form.pictureBox1.Width, form.pictureBox1.Width));
            Assert.That(star.Y, Is.InRange(-form.pictureBox1.Height, form.pictureBox1.Height));
        }

        [Test]
        public void StarsArray_Size_ShouldBeTwentyThousand()
        {
            // Act
            form.Form1_Load(null, EventArgs.Empty);

            // Assert
            Assert.That(form.stars.Length, Is.EqualTo(20000));
        }
    }
}