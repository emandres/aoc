use std::fs::File;
use std::io::Read;

fn parse_digits(input: &str) -> Vec<u32> {
    input.chars().map(|c| c.to_digit(10).unwrap()).collect()
}

fn score(digits: Vec<u32>) -> u32 {
    let mut d = digits[digits.len() - 1];
    let mut sum = 0u32;
    for c in digits {
        if d == c {
            sum += c
        } else {
            d = c
        }
    }
    sum
}

fn score2(digits: Vec<u32>) -> u32 {
    let len = digits.len();
    let mut sum = 0u32;

    for i in 0..len {
        let j = (i + (len / 2)) % len;
        if digits[i] == digits[j] {
            sum += digits[i];
        }
    }
    sum
}

fn main() {
    let mut buffer = String::new();
    let mut file = File::open("input.txt").unwrap();
    file.read_to_string(&mut buffer).unwrap();
    println!("{}", score2(parse_digits(&buffer)));
}

#[cfg(test)]
mod test {
    use super::*;

    #[test]
    fn score_no_matches_0() {
        assert_eq!(score("1234"), 0);
    }

    #[test]
    fn score_four_matches() {
        assert_eq!(score("1111"), 4);
    }

    #[test]
    fn score_two_matches() {
        assert_eq!(score("1122"), 3);
    }

    #[test]
    fn score2_tests() {
        assert_eq!(score2("1212"), 6);
        assert_eq!(score2("1221"), 0);
        assert_eq!(score2("123425"), 4);
        assert_eq!(score2("123123"), 12);
        assert_eq!(score2("12131415"), 4);

    }
}
