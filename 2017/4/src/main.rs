use std::collections::HashSet;
use std::fs::File;
use std::io::Read;

fn is_valid_passphrase(phrase: &str) -> bool {
    let words = phrase.split_whitespace().collect::<Vec<&str>>();
    let mut unique_words = HashSet::<String>::new();
    for w in &words {
        let mut sorted_chars = w.chars().collect::<Vec<char>>();
        sorted_chars.sort();

        unique_words.insert(sorted_chars.into_iter().collect::<String>());
    }

    words.len() == unique_words.len()
}

fn main() {
    let mut buffer = String::new();
    let mut file = File::open("input.txt").unwrap();
    file.read_to_string(&mut buffer).unwrap();
    println!("{}", buffer.lines().filter(|line| is_valid_passphrase(line)).collect::<Vec<&str>>().len());
}

#[cfg(test)]
mod test {
    use super::*;

    #[test]
    fn unique_words_pass() {
        assert!(is_valid_passphrase("aa bb cc dd ee"));
        assert!(is_valid_passphrase("aa bb cc dd aaa"));
    }

    #[test]
    fn repeated_words_fail() {
        assert!(!is_valid_passphrase("aa bb cc dd aa"));
    }
}