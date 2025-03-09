import os
import glob

def update_copyright_header(file_path):
    """Updates the copyright header to match the actual filename."""
    filename = os.path.basename(file_path)
    
    with open(file_path, 'r') as f:
        content = f.read()
    
    # Replace the filename in the copyright line
    updated_content = content.replace(
        '<copyright file="IPNetworkUnitTest.cs" company="IPNetwork">',
        f'<copyright file="{filename}" company="IPNetwork">'
    )
    
    if content != updated_content:
        with open(file_path, 'w') as f:
            f.write(updated_content)
        print(f"Updated copyright header in {filename}")
    else:
        print(f"No changes needed for {filename}")

def main():
    # Get all C# files in the IPNetworkTest directory
    test_files = glob.glob('src/TestProject/IPNetworkTest/*.cs')
    
    # Update copyright header in each file
    for file_path in test_files:
        update_copyright_header(file_path)
    
    print(f"Processed {len(test_files)} files")

if __name__ == "__main__":
    main()