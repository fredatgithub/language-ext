# $BestFormBin is where the bestform.exe is compiled to 
BestFormBin=/home/paul/Documents/dev/best-form/bestform

# $LangExtRoot is where the source code root should be (i.e. c:\dev\language-ext)
LangExtRoot=/home/paul/Documents/dev/language-ext

# $LangExtDocs is where the docs root should be (i.e. c:\dev\louthy.github.io)
LangExtDocs=/home/paul/Documents/dev/louthy.github.io

echo cleaning the docs

rm -rf $LangExtDocs/language-ext/LanguageExt.Core
rm -rf $LangExtDocs/language-ext/LanguageExt.Parsec
rm -rf $LangExtDocs/language-ext/LanguageExt.FSharp
rm -rf $LangExtDocs/language-ext/LanguageExt.Rx
rm -rf $LangExtDocs/language-ext/LanguageExt.Sys

echo building the docs

dotnet build $BestFormBin/bestform.csproj -c Release
dotnet run --project $BestFormBin -c Release --no-build "LanguageExt.Core" "$LangExtRoot/LanguageExt.Core" "$LangExtDocs/language-ext" "https://github.com/louthy/language-ext/tree/main"
dotnet run --project $BestFormBin -c Release --no-build "LanguageExt.Parsec" "$LangExtRoot/LanguageExt.Parsec" "$LangExtDocs/language-ext" "https://github.com/louthy/language-ext/tree/main"
dotnet run --project $BestFormBin -c Release --no-build "LanguageExt.FSharp" "$LangExtRoot/LanguageExt.FSharp" "$LangExtDocs/language-ext" "https://github.com/louthy/language-ext/tree/main"
dotnet run --project $BestFormBin -c Release --no-build "LanguageExt.Rx" "$LangExtRoot/LanguageExt.Rx" "$LangExtDocs/language-ext" "https://github.com/louthy/language-ext/tree/main"
dotnet run --project $BestFormBin -c Release --no-build "LanguageExt.Sys" "$LangExtRoot/LanguageExt.Sys" "$LangExtDocs/language-ext" "https://github.com/louthy/language-ext/tree/main"

echo committing docs to git

cd $LangExtDocs

git add .
git commit -m "Language-ext documentation update"
git push

echo finished commmitting docs to git
