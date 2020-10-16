# `toolbox-cs`
> C# Toolbox - general coding utilities

## Features

- Date provider interface and implementation
- Common string adapter functions
- File path adapters for file processing

## Usage

`Date Providers`

```cs

            var filePath = Path.GetFullPath("C:/a/long/path/to/previousfilename.txt");
            var dateTimeProvider = new ConstantDateTimeProvider(new DateTime(2010, 1, 1, 13, 30, 30));
            var adapter = AdapterChain.Create(
                new InsertPreExtensionTimestampFilePathAdapter(dateTimeProvider),
                new ReplaceInFileNameFilePathAdapter("previous", "new"),
                new InsertPreExtensionFilePathAdapter("extra"),
                new AppendDirectoryFilePathAdapter("newdir"),
                new ModifyExtensionFilePathAdapter("csv")
                );
            var result = adapter.Adapt(filePath);
            // Results in "C:/a/long/path/to/newdir/newfilename20100101133030000extra.csv"

```

## Credits

## License

[MIT](LICENSE)