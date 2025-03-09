import re
import os
import glob

def get_class_name_from_filename(filename):
    """Convert filename to class name"""
    # Extract base name without extension
    base_name = os.path.basename(filename).replace('.cs', '')
    return base_name

def rename_class_in_file(file_path, old_class_name, new_class_name):
    """Rename the class in a file"""
    with open(file_path, 'r') as f:
        content = f.read()
    
    # Replace class declaration
    content = re.sub(
        r'public class ' + old_class_name,
        'public class ' + new_class_name,
        content
    )
    
    # Update class summary comment
    content = re.sub(
        r'/// <summary>\s*\n\s*/// IPNetworkUnitTest test every single method\.',
        f'/// <summary>\n    /// {new_class_name} tests',
        content
    )
    
    with open(file_path, 'w') as f:
        f.write(content)
    
    print(f"Updated {file_path}: {old_class_name} -> {new_class_name}")

def main():
    # Get all test files in the IPNetworkTest directory
    test_files = glob.glob('src/TestProject/IPNetworkTest/*.cs')
    
    # Exclude README.md and any other non-cs files
    test_files = [f for f in test_files if f.endswith('.cs')]
    
    # Rename class in each file
    for file_path in test_files:
        new_class_name = get_class_name_from_filename(file_path)
        rename_class_in_file(file_path, 'IPNetworkUnitTest', new_class_name)

if __name__ == "__main__":
    main()
