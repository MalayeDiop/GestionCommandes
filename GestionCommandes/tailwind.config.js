/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Pages/**/*.cshtml', // Pour les Razor Pages
    './Views/**/*.cshtml', // Pour MVC
    './wwwroot/**/*.html', // Si vous avez des fichiers HTML
  ],
  theme: {
    extend: {
      colors: {
        teal: '#1D797E',
      },
      boxShadow: {
        'custom': '4px 4px 10px rgba(0, 0, 0, 0.2)',
      },
    },
  },
  plugins: [],
}

