Localizing this application
===========================

Files:
    o languages/src/ichorsuppliers.pot
      - This is the master translation template file. This is the file that is sent to a translation company to be
        translated into a local language
      - All new strings for translation must be added to this file.

    o languages/src/{lang}.po
      - These are the translations for the given language.

    o languages/{lang}.json
      - These files are created by the build process. A grunt task converts the .po files into json files suitable
        for use with angular-translate
      - The visual studio project has been setup such that it will watch for changes to the .po files and automatically
        generate the json files.

Adding Translations:
    - Send the .pot file off to the translation company to be translated
    - Save the returned file as {lang}.po in languages/src
    - Open app.config.js and add the new language to registerAvailableLanguageKeys along with any browser
      language mappings required (below it)
