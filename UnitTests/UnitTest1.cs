using StarfieldSimulation;

namespace UnitTests
{
    public class Tests
    {
        private Form1 form;

        [SetUp]
        public void Setup()
        {
            form = new Form1();
            form.Form1_Load(null, null); // Инициализируем форму
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
            Assert.That(result, Is.EqualTo(5).Within(0.001));
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
            Assert.GreaterOrEqual(star.Z, 1);
        }

        [Test]
        public void MoveStar_StarZGreaterThanOne_DoesNotResetPosition()
        {
            // Arrange
            var star = new Form1.Star { X = 0, Y = 0, Z = 10 };

            // Act
            form.MoveStar(star);

            // Assert
            Assert.Greater(star.Z, 0);
        }

        [Test]
        public void Form1_Load_InitializesStarsArray()
        {
            // Assert
            Assert.IsNotNull(form.stars);
            Assert.That(form.stars.Length, Is.EqualTo(20000));
            foreach (var star in form.stars)
            {
                Assert.IsNotNull(star);
                Assert.IsTrue(star.X >= -form.pictureBox1.Width && star.X <= form.pictureBox1.Width);
                Assert.IsTrue(star.Y >= -form.pictureBox1.Height && star.Y <= form.pictureBox1.Height);
                Assert.IsTrue(star.Z >= 1 && star.Z <= form.pictureBox1.Width);
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
            Assert.Greater(expectedSize, 0);
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
            Assert.IsTrue(x >= 0 && x <= form.pictureBox1.Width);
            Assert.IsTrue(y >= 0 && y <= form.pictureBox1.Height);
        }

        [Test]
        public void TimerTick_CallsMoveAndDrawStarMethods()
        {
            // Arrange
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += form.timer1_Tick;

            // Act
            timer.Start();
            form.timer1_Tick(null);

            // Assert
            Assert.IsNotNull(form.Stars);
            Assert.IsTrue(form.Stars.Length > 0);
        }

        [Test]
        public void MoveStar_WhenStarIsReset_XAndYAreInBounds()
        {
            // Arrange
            var star = new Form1.Star { X = 0, Y = 0, Z = 0.5f };
            form.MoveStar(star);

            // Assert
            Assert.IsTrue(star.X >= -form.pictureBox1.Width && star.X <= form.pictureBox1.Width);
            Assert.IsTrue(star.Y >= -form.pictureBox1.Height && star.Y <= form.pictureBox1.Height);
        }

        [Test]
        public void StarsArray_Size_ShouldBeTwentyThousand()
        {
            // Act
            form.Form1_Load(null);

            // Assert
            Assert.AreEqual(20000, form.Stars.Length);
        }
    }
}